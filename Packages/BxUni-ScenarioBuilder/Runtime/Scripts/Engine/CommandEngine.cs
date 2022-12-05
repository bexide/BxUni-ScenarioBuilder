using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using BxUni.ScenarioBuilderInternal;

namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// コマンドの処理を行う
    /// </summary>
    public class CommandEngine
    {
        #region Property

        /// <summary>
        /// コマンドに対する実行メソッドをまとめたテーブル
        /// </summary>
        Dictionary<Type, RunnerData> RunnerTable { get; set; }
            = new Dictionary<Type, RunnerData>();

        /// <summary>
        /// IJumpCommandを実装したコマンドに対する実行メソッドをまとめたテーブル
        /// </summary>
        Dictionary<Type, RunnerData> JumpRunnerTable { get; set; }
             = new Dictionary<Type, RunnerData>();

        /// <summary>
        /// 実行するシナリオデータ
        /// </summary>        
        BaseCommand[] RunCommands { get; set; } = new BaseCommand[0];

        /// <summary>
        /// キャンセル
        /// </summary>
        CancellationTokenSource Cancel { get; set; } = null;

        /// <summary>
        /// リセット中かどうか
        /// </summary>
        bool IsResetRunning { get; set; } = false;

        #endregion //Property

        #region IObservable

        /// <summary>
        /// シナリオ開始時に通知
        /// </summary>
        public IObservable<Unit> OnStart => m_onStartSubject.Where(_ => !IsResetRunning);
        Subject<Unit> m_onStartSubject = new Subject<Unit>();

        /// <summary>
        /// シナリオ終了時に通知
        /// </summary>
        public IObservable<Unit> OnEnd => m_onEndSubject.Where(_ => !IsResetRunning);
        Subject<Unit> m_onEndSubject = new Subject<Unit>();

        /// <summary>
        /// リセット通知
        /// </summary>
        public IObservable<Unit> OnReset => m_onResetSubject;
        Subject<Unit> m_onResetSubject = new Subject<Unit>();

        #endregion

        #region event

        /// <summary>
        /// 各Runner内のResetRunner処理が終わったあとに実行する処理を登録するためのイベント
        /// </summary>
        public event Func<UniTask> postResetTask;

        #endregion

        /// <summary>
        /// リセットを通知します。
        /// </summary>
        internal async UniTask ResetTask()
        {
            IsResetRunning = true;

            //実行中のシナリオを終了する
            Skip();

            //各Runner内のリセット
            foreach (var runner in RunnerTable.Values)
            {
                runner.Reset();
            }

            //全てのランナーがリセットされた後に実行する処理
            if(postResetTask != null)
            {
                await postResetTask.Invoke();
            }

            m_onResetSubject.OnNext(Unit.Default);

            IsResetRunning = false;
        }

        /// <summary>
        /// 再生中のシナリオをキャンセルして終了します。
        /// </summary>
        public void Skip()
        {
            Cancel?.Cancel();
        }

        /// <summary>
        /// Engineの初期化処理
        /// </summary>
        /// <param name="commands">実行するステートのパラメータ群</param>
        /// <param name="runners">実行対象</param>
        public void Initialize(BaseCommand[] commands, BaseCommandRunner[] runners)
        {
            Debug.Assert(commands != null, "The first argument \"commands\" is null.");
            if (commands == null) { return; }

            //プロパティの初期化
            RunCommands = commands;
            RunnerTable.Clear();
            JumpRunnerTable.Clear();
            Cancel = new CancellationTokenSource();

            #region 関数内関数定義
            MethodInfo[] FindMethodInfos(BaseCommandRunner runner)
            {
                return runner
                    .GetType()
                    .GetMethods()
                    .Where(AttributeUtility.HasMethodAttribute<CommandRunnerAttribute>)
                    .ToArray();
            }
            void RegistTable(BaseCommandRunner runner, MethodInfo method)
            {
                var attribute     = AttributeUtility.GetMethodAttribute<CommandRunnerAttribute>(method);
                var commandType = attribute.RunCommandType;

                var runnerData = new RunnerData(runner, method);

                if (commandType.GetInterfaces().Contains(typeof(IJumpCommand)))
                {
                    JumpRunnerTable.Add(commandType, runnerData);
                }
                else
                {
                    RunnerTable.Add(commandType, runnerData);
                }
            }
            #endregion

            //commandに対して実行対象のRunnerとMethodを紐づけてテーブルに登録していく
            var allRunners = runners
                .SelectMany(runner => runner.GetComponents<BaseCommandRunner>())
                .Distinct()
                .ToArray();
            foreach (var runner in allRunners)
            {
                var methodInfos = FindMethodInfos(runner);
                foreach (var method in methodInfos)
                {
                    RegistTable(runner, method);
                }
            }
        }

        /// <summary>
        /// Engineの初期化処理
        /// </summary>
        /// <param name="scenario">実行するシナリオ</param>
        /// <param name="runners">実行対象</param>
        public void Initialize(ScenarioData scenario, BaseCommandRunner[] runners)
        {
            Debug.Assert(scenario != null, "The first argument \"scenario\" is null.");
            if(scenario == null) { return; }

            Initialize(scenario.Commands.ToArray(), runners);
        }

        /// <summary>
        /// 実行（待機無し）
        /// </summary>
        /// <param name="ct"></param>
        public void Run(CancellationToken ct = default) => RunTask(ct).Forget();

        /// <summary>
        /// 実行（終了まで待機）
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async UniTask RunTask(CancellationToken ct = default)
        {
            Debug.Assert(
                RunCommands != null && RunCommands.Any(),
                "Pre-execute the \"Initialize()\" method"
            );
            if(RunCommands == null || !RunCommands.Any()) { return; }

            //開始時の通知
            m_onStartSubject.OnNext(Unit.Default);

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct, Cancel.Token);
            try
            {
                await RunTaskImpl(cts.Token);
            }
            catch(OperationCanceledException)
            {
                if (ct.IsCancellationRequested)
                {
                    Debug.LogWarning("External CancellationToken cancelled.");
                }
                else
                {
                    Debug.LogWarning("Skipped.");
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                cts.Cancel();
            }

            //終了時の通知
            m_onEndSubject.OnNext(Unit.Default);
        }

        async UniTask RunTaskImpl(CancellationToken ct)
        {
            var commands     = RunCommands;
            int commandCount = commands.Length;

            for (int index = 0; index < commandCount; index++)
            {
                //キャンセルされている場合はThrow
                ct.ThrowIfCancellationRequested();
                index = await RunCommandTask(commands, index, ct);
            }
        }

        async UniTask<int> RunCommandTask(BaseCommand[] commands, int targetIndex, CancellationToken ct)
        {
            var (command, index) = await DoJumpTask(commands[targetIndex], targetIndex, commands, ct);
 
            //Runnerが存在しなければ実行できないのでコンティニュー
            if (!RunnerTable.TryGetValue(command.GetType(), out var runnerData))
            {
                return index;
            }

            //実行
            if (runnerData.IsReturnTypeCompair(typeof(void)))
            {
                runnerData.Invoke(command);
            }
            else if (runnerData.IsReturnTypeCompair(typeof(UniTask)))
            {
                var task = runnerData.Invoke<UniTask>(command, ct);
                await task;
            }

            return index;
        }

        async UniTask<(BaseCommand command, int index)> DoJumpTask(
            BaseCommand currentCommand, int currentIndex, BaseCommand[] commands, CancellationToken ct
        )
        {
            //処理するCommandがジャンプ系Commandでなければ今のコマンドのまま
            if(!(currentCommand is IJumpCommand jumpCommand))
            {
                return (currentCommand, currentIndex);
            }

            //処理するRunnerが存在しなければエラーで返す
            if(!JumpRunnerTable.TryGetValue(jumpCommand.GetType(), out var runnerData))
            {
                Debug.LogError($"There is no Runner to process the command. [{jumpCommand.GetType()}]");
                return (currentCommand, currentIndex);
            }

            #region 関数内関数定義

            bool FindCommand(string targetLabel, out (BaseCommand, int) result)
            {
                result   = default;
                int size = commands.Length;
                for(int i=0; i<size; i++)
                {
                    var command = commands[i];
                    if(!(command is LabelCommand labelCommand)) { continue; }
                    if(labelCommand.Name == targetLabel)
                    {
                        result = (labelCommand, i);
                        return true;
                    }
                }
                Debug.LogError($"Not found Label [{targetLabel}]");
                return false;
            }

            #endregion

            if(runnerData.IsReturnTypeCompair(typeof(string)))
            {
                string targetLabel = runnerData.Invoke<string>(jumpCommand);
                return FindCommand(targetLabel, out var result) 
                    ? result : (currentCommand, currentIndex);
            }
            else if (runnerData.IsReturnTypeCompair(typeof(UniTask<string>)))
            {
                var task = runnerData.Invoke<UniTask<string>>(jumpCommand, ct);
                string targetLabel = await task;

                return FindCommand(targetLabel, out var result)
                    ? result : (currentCommand, currentIndex);
            }
            else
            {
                throw new Exception("予期せぬエラーが発生しました。");
            }
        }

    }
}
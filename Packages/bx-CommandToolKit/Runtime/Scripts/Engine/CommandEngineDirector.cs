using System;
using System.Threading;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace BX.CommandToolKit
{
    public sealed class CommandEngineDirector : MonoBehaviour
    {
#pragma warning disable 0649
        [Header("=== 再生するシナリオ ===")]
        [SerializeField] ScenarioData m_scenarioAsset;

        [Header("=== Awake時に再生を行うかどうか ===")]
        [SerializeField] bool m_playOnAwake = false;
#pragma warning restore 0649

        #region property

        /// <summary>
        /// 再生するシナリオデータ
        /// </summary>
        public ScenarioData scenarioAsset
        {
            get => m_scenarioAsset;
            set => m_scenarioAsset = value;
        }

        /// <summary>
        /// Awake時に再生を開始します
        /// </summary>
        public bool playOnAwake
        {
            get => m_playOnAwake;
            set => m_playOnAwake = value;
        }

        CommandEngine Engine { get; set; } = new CommandEngine();

        #endregion

        #region Event

        /// <summary>開始時に発火するEvent</summary>
        public event Action onStart;

        /// <summary>終了時に発火するEvent</summary>
        public event Action onEnd;

        /// <summary>
        /// リセット実行前に発火するEvent
        /// <para>OnResetの通知が発行される前</para>
        /// </summary>
        public event Func<UniTask> preResetTask;
        
        #endregion

        #region IObservable

        /// <summary>開始時の通知</summary>
        public IObservable<Unit> OnStart => Engine.OnStart;

        /// <summary>終了時の通知</summary>
        public IObservable<Unit> OnEnd => Engine.OnEnd;

        public IObservable<Unit> OnReset => Engine.OnReset;

        #endregion

        void Awake()
        {
            OnStart.Subscribe(_ => onStart?.Invoke()).AddTo(this);
            OnEnd.Subscribe(_ => onEnd?.Invoke()).AddTo(this);

            Engine.postResetTask += PreResetTask;

            OnReset.Subscribe(_ => 
            {
                Initialize();
                Play(this.GetCancellationTokenOnDestroy());
            });

            if (playOnAwake)
            {
                Initialize();
                Play(this.GetCancellationTokenOnDestroy());
            }
        }

        /// <summary>
        /// 初期化処理。再生前に必ず一度呼んでください。
        /// </summary>
        /// <param name="scenarioData">引数に指定すると,そのシナリオを再生します。nullの場合Inspector上で指定したファイルが呼ばれます。</param>
        public void Initialize(ScenarioData scenarioData = null)
        {
            if(scenarioData != null)
            {
                scenarioAsset = scenarioData;
            }

            var runners = FindObjectsOfType<BaseCommandRunner>();
            Engine.Initialize(scenarioAsset, runners);
        }

        /// <summary>
        /// scenarioAssetプロパティに設定されているシナリオを再生します。
        /// </summary>
        /// <param name="ct">キャンセルトークン</param>
        public void Play(CancellationToken ct = default)
        {
            PlayTask(ct).Forget();
        }

        /// <summary>
        /// scenarioAssetプロパティに設定されているシナリオを再生します。
        /// <para>再生終了まで待機することが可能です。</para>
        /// </summary>
        /// <param name="ct">キャンセルトークン</param>
        /// <returns></returns>
        public async UniTask PlayTask(CancellationToken ct = default)
        {
            await Engine.RunTask(ct);
        }

        /// <summary>
        /// 再生途中のシナリオをスキップします。
        /// </summary>
        public void Skip()
        {
            Engine.Skip();
        }

        /// <summary>
        /// リセットして最初から再生しなおします。
        /// </summary>
        internal void ResetFlow()
        {
            Engine.ResetTask().Forget();
        }

        /// <summary>
        /// リセット実行前に行われる
        /// </summary>
        /// <returns></returns>
        internal async UniTask PreResetTask()
        {
            if (preResetTask == null) { return; }
            await preResetTask.Invoke();
        }

    }
}
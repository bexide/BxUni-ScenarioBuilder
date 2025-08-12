//BeXide 2022-11-29
//by MurakamiKazuki

using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

//UniTaskが使用出来る場合
#if SCENARIOBUILDER_UNITASK_SUPPORT
    using Cysharp.Threading.Tasks;
#endif

namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// コマンドを実行するコンポーネント
    /// </summary>
    [DefaultExecutionOrder(-10)]
    public sealed partial class CommandEngineDirector : MonoBehaviour
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

        CancellationTokenSource DestroyCts { get; set; } = new CancellationTokenSource();

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
#if SCENARIOBUILDER_UNITASK_SUPPORT
        public event Func<UniTask> onPostResetTask;
#else
        public event Func<Task> onPostResetTask;
#endif

        #endregion

        #region Method

        void Awake()
        {
            Engine.onStartEvent.AddListener(() => onStart?.Invoke());
            Engine.onEndEvent.AddListener(() => onEnd?.Invoke());
            Engine.onResetCompleted.AddListener(() =>
            {
                Initialize();
                Play(GetCancellationTokenOnDestroy());
            });
            Engine.onPostResetTask += PostResetTask;

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

#if UNITY_6000_0_OR_NEWER
            var runners = FindObjectsByType<BaseCommandRunner>(FindObjectsSortMode.None);
#else
            var runners = FindObjectsOfType<BaseCommandRunner>();
#endif
            Engine.Initialize(scenarioAsset, runners);
        }

        /// <summary>
        /// scenarioAssetプロパティに設定されているシナリオを再生します。
        /// </summary>
        /// <param name="ct">キャンセルトークン</param>
        public void Play(CancellationToken ct = default)
        {
            Engine.Run(ct);
        }

        /// <summary>
        /// scenarioAssetプロパティに設定されているシナリオを再生します。
        /// <para>再生終了まで待機することが可能です。</para>
        /// </summary>
        /// <param name="ct">キャンセルトークン</param>
        /// <returns></returns>
#if SCENARIOBUILDER_UNITASK_SUPPORT
        public async UniTask PlayTask(CancellationToken ct = default)
#else
        public async Task PlayTask(CancellationToken ct = default)
#endif
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
#if SCENARIOBUILDER_UNITASK_SUPPORT
            Engine.ResetTask().Forget();
#else
            _ = Engine.ResetTask();
#endif
        }

        /// <summary>
        /// リセット実行後に行われる
        /// </summary>
        /// <returns></returns>
#if SCENARIOBUILDER_UNITASK_SUPPORT
        internal async UniTask PostResetTask()
#else
        internal async Task PostResetTask()
#endif
        {
            if (onPostResetTask == null) { return; }
            await onPostResetTask.Invoke();
        }

        CancellationToken GetCancellationTokenOnDestroy()
        {
            return DestroyCts.Token;
        }

        void OnDestroy()
        {
            DestroyCts?.Cancel();
            DestroyCts?.Dispose();
        }

        #endregion
    }
}
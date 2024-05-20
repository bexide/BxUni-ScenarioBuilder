// BeXide 2024-05-20
// by MurakamiKazuki

// R3が使用できる場合

#if SCENARIOBUILDER_R3_SUPPORT
using R3;

namespace BxUni.ScenarioBuilder
{
    internal partial class CommandEngine
    {
        /// <summary>
        /// シナリオ開始時に通知
        /// </summary>
        public Observable<Unit> OnStart => onStartEvent.AsObservable().Where(_ => !IsResetRunning);

        /// <summary>
        /// シナリオ終了時に通知
        /// </summary>
        public Observable<Unit> OnEnd => onEndEvent.AsObservable().Where(_ => !IsResetRunning);

        /// <summary>
        /// リセット通知
        /// </summary>
        public Observable<Unit> OnResetCompleted => onResetCompleted.AsObservable();
    }

}

#endif //SCENARIOBUILDER_R3_SUPPORT

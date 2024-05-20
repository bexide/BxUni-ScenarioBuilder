//BeXide 2022-12-14
//by MurakamiKazuki

//UniRxが使用出来るかつR3が入っていない場合

#if SCENARIOBUILDER_UNIRX_SUPPORT && !SCENARIOBUILDER_R3_SUPPORT
using System;
using UniRx;

namespace BxUni.ScenarioBuilder
{
    internal partial class CommandEngine
    {
        /// <summary>
        /// シナリオ開始時に通知
        /// </summary>
        public IObservable<Unit> OnStart => onStartEvent.AsObservable().Where(_ => !IsResetRunning);

        /// <summary>
        /// シナリオ終了時に通知
        /// </summary>
        public IObservable<Unit> OnEnd => onEndEvent.AsObservable().Where(_ => !IsResetRunning);

        /// <summary>
        /// リセット通知
        /// </summary>
        public IObservable<Unit> OnResetCompleted => onResetCompleted.AsObservable();

    }
}

#endif //SCENARIOBUILDER_UNIRX_SUPPORT
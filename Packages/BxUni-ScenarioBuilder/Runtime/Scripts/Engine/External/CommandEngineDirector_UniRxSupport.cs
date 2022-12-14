//UniRxが使用出来る場合

#if SCENARIOBUILDER_UNIRX_SUPPORT
using System;
using UniRx;

namespace BxUni.ScenarioBuilder
{
    public partial class CommandEngineDirector
    {
        /// <summary>開始時の通知</summary>
        public IObservable<Unit> OnStart => Engine.OnStart;

        /// <summary>終了時の通知</summary>
        public IObservable<Unit> OnEnd => Engine.OnEnd;

        /// <summary>リセット時の通知</summary>
        public IObservable<Unit> OnResetCompleted => Engine.OnResetCompleted;

    }
}

#endif //SCENARIOBUILDER_UNIRX_SUPPORT
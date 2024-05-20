//BeXide 2022-12-14
//by MurakamiKazuki

//UniRxが使用出来るかつR3が入っていない場合

#if SCENARIOBUILDER_UNIRX_SUPPORT && !SCENARIOBUILDER_R3_SUPPORT
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
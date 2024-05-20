// BeXide 2024-05-20
// by MurakamiKazuki

// R3が使用できる場合

#if SCENARIOBUILDER_R3_SUPPORT
using R3;

namespace BxUni.ScenarioBuilder
{
    public partial class CommandEngineDirector
    {
        /// <summary>開始時の通知</summary>
        public Observable<Unit> OnStart => Engine.OnStart;

        /// <summary>終了時の通知</summary>
        public Observable<Unit> OnEnd => Engine.OnEnd;

        /// <summary>リセット時の通知</summary>
        public Observable<Unit> OnResetCompleted => Engine.OnResetCompleted;

    }
}

#endif //SCENARIOBUILDER_R3_SUPPORT

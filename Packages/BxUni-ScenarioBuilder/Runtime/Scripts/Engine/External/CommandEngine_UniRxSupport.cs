//UniRxが使用出来る場合

#if SCENARIOBUILDER_UNIRX_SUPPORT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
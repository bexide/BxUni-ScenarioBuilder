using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class DemoProc : MonoBehaviour
    {
        [SerializeField] CommandEngineDirector m_director;
        [SerializeField] ScenarioData m_scenarioAsset;

        void Awake()
        {
            //各コールバックの登録
            m_director.onStart         += OnStart;
            m_director.onEnd           += OnEnd;
            m_director.onPostResetTask += OnPostResetTask;

            //初期化設定
            m_director.Initialize(m_scenarioAsset);
            
            //再生
            m_director.Play();
        }

        /// <summary>
        /// シナリオ実行開始時に呼ばれるイベント
        /// </summary>
        void OnStart()
        {
            Debug.Log($"<color=lime>> {nameof(OnStart)}</color>");
        }

        /// <summary>
        /// シナリオ実行終了時に呼ばれるイベント
        /// </summary>
        void OnEnd()
        {
            Debug.Log($"<color=lime>> {nameof(OnEnd)}</color>");
        }

        /// <summary>
        /// リセット時に呼ばれるイベント
        /// </summary>
        /// <returns></returns>
        async Task OnPostResetTask()
        {
            await Task.CompletedTask;
            Debug.Log($"<color=lime>> {nameof(OnPostResetTask)}</color>");
        }
    }
}
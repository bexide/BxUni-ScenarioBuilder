using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    [System.Serializable]
    public class ActiveSwitchCommand : BaseCommand
    {
        [Header("固有ID"), PrefabIDSelector]
        [SerializeField] string m_id;

        [Header("表示/非表示")]
        [SerializeField] bool m_isActive = true;

        /// <summary>
        /// 固有ID
        /// </summary>
        public string ID => m_id;

        /// <summary>
        /// 表示/非表示
        /// </summary>
        public bool Active => m_isActive;
    }
}
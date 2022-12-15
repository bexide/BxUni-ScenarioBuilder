using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    [System.Serializable]
    public class SetAnimationCommand : BaseCommand
    {
        [Header("固有ID"), PrefabIDSelector]
        [SerializeField] string m_id;

        [SerializeField] string m_animationName;

        /// <summary>
        /// 固有ID
        /// </summary>
        public string ID => m_id;

        /// <summary>
        /// アニメーション名
        /// </summary>
        public string AnimationName => m_animationName;
    }
}
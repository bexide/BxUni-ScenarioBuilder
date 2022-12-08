using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    [System.Serializable]
    public class BackgroundChangeCommand : BaseCommand
    {
        [Header("表示する画像")]
        [SerializeField] Sprite m_backgroundSprite;

        /// <summary>
        /// 表示する画像
        /// </summary>
        public Sprite BackgroundSprite => m_backgroundSprite;
    }
}
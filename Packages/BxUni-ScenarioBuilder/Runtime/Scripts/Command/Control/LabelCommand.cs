//BeXide 2022-12-05
//by MurakamiKazuki

using UnityEngine;

namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// フラグの役割をもつコマンド。
    /// <para>JumpCommandの飛び先等で使用する</para>
    /// </summary>
    [System.Serializable]
    [CommandTooltip("ジャンプ先のラベルを設定します。")]
    public class LabelCommand : BaseCommand
    {
#pragma warning disable 0649
        [SerializeField] string m_name;
#pragma warning restore 0649

        /// <summary>
        /// ラベル名
        /// </summary>
        public string Name => m_name;

        protected override string GetDefaultGUIText()
        {
            return $"* {Name}";
        }
    }
}
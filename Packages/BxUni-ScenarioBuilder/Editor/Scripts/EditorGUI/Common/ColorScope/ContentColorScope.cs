using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// スコープ内のGUIのコンテンツの色を変える
    /// </summary>
    internal class ContentColorScope : BaseColorScope
    {
        internal ContentColorScope(Color setColor) : base(setColor) { }

        internal override Color settingColor
        {
            get => GUI.contentColor;
            set => GUI.contentColor = value;
        }
    }
}
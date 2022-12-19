//BeXide 2022-11-29
//by MurakamiKazuki

using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// スコープ内のGUIの背景色を変える
    /// </summary>
    internal class BackgroundColorScope : BaseColorScope
    {
        internal BackgroundColorScope(Color setColor) : base(setColor) { }

        internal override Color settingColor
        {
            get => GUI.backgroundColor;
            set => GUI.backgroundColor = value;
        }
    }
}
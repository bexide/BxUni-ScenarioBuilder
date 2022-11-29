using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    /// <summary>
    /// スコープ内のGUIの色を変える
    /// </summary>
    internal class ColorScope : BaseColorScope
    {
        internal ColorScope(Color setColor) : base(setColor) { }

        internal override Color settingColor
        {
            get => GUI.color;
            set => GUI.color = value;
        }
    }
}
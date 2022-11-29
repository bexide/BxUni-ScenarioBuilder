using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    internal abstract class BaseColorScope : GUI.Scope
    {
        internal Color color { get; set; }

        internal Color originColor { get; set; }

        internal abstract Color settingColor { get; set; }

        public BaseColorScope(Color setColor)
        {
            color = setColor;

            originColor = settingColor;

            settingColor = setColor;
        }

        protected override void CloseScope()
        {
            settingColor = originColor;
        }
    }
}
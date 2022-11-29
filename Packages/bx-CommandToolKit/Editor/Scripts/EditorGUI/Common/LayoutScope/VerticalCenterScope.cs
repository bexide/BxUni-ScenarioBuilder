using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    internal class VerticalCenterScope : GUI.Scope
    {
        internal VerticalCenterScope()
        {
            GUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            GUILayout.FlexibleSpace();
        }

        protected override void CloseScope()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}
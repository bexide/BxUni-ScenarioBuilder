using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    internal class HorizontalCenterScope : GUI.Scope
    {
        internal HorizontalCenterScope()
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
        }

        protected override void CloseScope()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}

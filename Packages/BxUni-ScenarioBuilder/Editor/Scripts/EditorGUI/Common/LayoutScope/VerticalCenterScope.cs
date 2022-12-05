using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
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
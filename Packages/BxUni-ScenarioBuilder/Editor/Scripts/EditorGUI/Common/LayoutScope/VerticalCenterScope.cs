using System.Linq;
using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class VerticalCenterScope : GUI.Scope
    {
        internal VerticalCenterScope(params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options.Append(GUILayout.ExpandHeight(true)).ToArray());
            GUILayout.FlexibleSpace();
        }

        protected override void CloseScope()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}
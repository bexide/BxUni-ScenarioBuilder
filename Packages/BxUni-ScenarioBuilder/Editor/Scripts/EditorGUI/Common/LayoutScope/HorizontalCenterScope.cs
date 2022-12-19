//BeXide 2022-11-29
//by MurakamiKazuki

using System.Linq;
using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class HorizontalCenterScope : GUI.Scope
    {
        internal HorizontalCenterScope(params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options.Append(GUILayout.ExpandWidth(true)).ToArray());
            GUILayout.FlexibleSpace();
        }

        protected override void CloseScope()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}

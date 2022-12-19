//BeXide 2022-12-12
//by MurakamiKazuki

using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class PathColumn : MultiColumnHeaderState.Column
    {
        internal PathColumn()
        {
            width = 200;
            headerContent = new GUIContent("Path");
        }
    }
}
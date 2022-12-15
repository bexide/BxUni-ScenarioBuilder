using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class PathColumn : MultiColumnHeaderState.Column
        , IHasColumnIndex
    {
        ColumnIndex IHasColumnIndex.Index => ColumnIndex.Path;
    
        internal PathColumn()
        {
            width = 200;
            headerContent = new GUIContent("Path");
        }
    }
}
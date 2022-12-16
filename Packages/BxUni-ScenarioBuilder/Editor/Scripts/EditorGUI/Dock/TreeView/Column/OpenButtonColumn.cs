using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class OpenButtonColumn : MultiColumnHeaderState.Column
    {     
        internal OpenButtonColumn()
        {
            width                 = 30;
            headerContent         = new GUIContent("Open");
            allowToggleVisibility = false;
            sortedAscending       = false;
            canSort               = false;
        }
    }
}
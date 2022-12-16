using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class NameColumn : MultiColumnHeaderState.Column
    {    
        internal NameColumn()
        {
            width = 80;
            headerContent = new GUIContent("Name");
        }
    }
}
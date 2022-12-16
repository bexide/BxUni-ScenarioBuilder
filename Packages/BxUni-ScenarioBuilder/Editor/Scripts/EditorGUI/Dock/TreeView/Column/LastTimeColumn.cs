using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class LastTimeColumn : MultiColumnHeaderState.Column
    {
        internal LastTimeColumn()
        {
            width = 80;
            headerContent = new GUIContent("Last Time");
        }
    }
}
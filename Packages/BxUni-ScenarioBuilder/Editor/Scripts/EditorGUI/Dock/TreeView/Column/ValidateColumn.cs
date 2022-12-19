//BeXide 2022-12-15
//by MurakamiKazuki

using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ValidateColumn : MultiColumnHeaderState.Column
    {
        internal ValidateColumn()
        {
            width = 30;
            headerContent = new GUIContent("Validate");
        }
    }
}

//BeXide 2022-12-12
//by MurakamiKazuki

using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class BytesColumn : MultiColumnHeaderState.Column
    {
        internal BytesColumn()
        {
            width = 30;
            headerContent = new GUIContent("Bytes");
        }
    }
}
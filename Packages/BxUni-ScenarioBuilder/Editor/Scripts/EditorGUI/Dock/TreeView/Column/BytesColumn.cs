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
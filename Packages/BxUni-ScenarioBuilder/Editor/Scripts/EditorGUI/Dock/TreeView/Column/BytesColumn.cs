using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class BytesColumn : MultiColumnHeaderState.Column
        , IHasColumnIndex
    {
        ColumnIndex IHasColumnIndex.Index => ColumnIndex.Bytes;
        
        internal BytesColumn()
        {
            width = 30;
            headerContent = new GUIContent("Bytes");
        }
    }
}
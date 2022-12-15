using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    public class ValidateColumn : MultiColumnHeaderState.Column
        , IHasColumnIndex
    {
        ColumnIndex IHasColumnIndex.Index => ColumnIndex.Validate;

        internal ValidateColumn()
        {
            width = 30;
            headerContent = new GUIContent("Validate");
        }
    }
}

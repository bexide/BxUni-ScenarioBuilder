//BeXide 2022-11-14
//by MurakamiKazuki

using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace BX.CommandToolKit.EditorInternal
{
    internal class MonoScriptSelectDropdownItem : AdvancedDropdownItem
    {
        internal MonoScript Script { get; }

        internal MonoScriptSelectDropdownItem(MonoScript script) 
            : base(script.GetClass().Name)
        {
            Script = script;

            var icon = EditorGUIUtility.TrIconContent("cs Script Icon");
            this.icon = icon?.image as Texture2D;
        }
    }
}
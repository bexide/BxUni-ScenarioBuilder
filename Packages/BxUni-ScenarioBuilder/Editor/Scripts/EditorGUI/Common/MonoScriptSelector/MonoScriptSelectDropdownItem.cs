using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
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
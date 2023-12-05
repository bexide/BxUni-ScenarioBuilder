//BeXide 2023-12-04
//by MurakamiKazuki

using System;
using UnityEditor;
using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal static class MonoScriptSelectDropdownDrawer
    {
        internal static void Draw(string name, Action<MonoScript> onSelected)
        {
            var rect = GUILayoutUtility.GetLastRect();
            Draw(rect, name, onSelected);
        }


        internal static void Draw(Rect rect, string name, Action<MonoScript> onSelected)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "None (MonoScript)";
            }

            var style = new GUIStyle(EditorStyles.popup)
            {
                fontStyle = FontStyle.Bold
            };

            if (GUI.Button(rect, name, style))
            {
                var dropdown = new MonoScriptSelectDropdown(onSelected);
                dropdown.Show(rect);
            }
        }

    }
}
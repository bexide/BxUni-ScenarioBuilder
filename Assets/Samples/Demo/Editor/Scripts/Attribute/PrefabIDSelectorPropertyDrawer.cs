using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    [CustomPropertyDrawer(typeof(PrefabIDSelectorAttribute))]
    public class PrefabIDSelectorPropertyDrawer : PropertyDrawer
    {

        static readonly string[] k_defaultList = new string[]
        {
            "Selected..."
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(property.propertyType != SerializedPropertyType.String) 
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var so = property.serializedObject;
            if(!(so.targetObject is ScenarioData scenarioData))
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            string[] list = k_defaultList
                .Concat(
                    scenarioData.Commands
                        .Where(cmd => cmd is PrefabSpawnSetupCommand)
                        .SelectMany(cmd => (cmd as PrefabSpawnSetupCommand).Setups)
                        .Select(setup => setup.ID)
                        .Where(id => !string.IsNullOrEmpty(id))
                )
                .ToArray();
            if (!list.Any())
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            int currentIndex = 0;
            try
            {
                currentIndex = System.Array.IndexOf(list, property.stringValue);
            }
            catch { }

            currentIndex = Mathf.Clamp(currentIndex, 0, list.Length - 1);
            currentIndex = EditorGUI.Popup(position, label.text, currentIndex, list);
            property.stringValue = list[currentIndex];
        }

    }
}
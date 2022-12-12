using System.Linq;
using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [CustomPropertyDrawer(typeof(LabelCommandAttribute))]
    internal sealed class LabelCommandAttributeDrawer : PropertyDrawer
    {

        static readonly string[] k_defaultList = new string[]
        {
            "Selected...",
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var so = property.serializedObject;
            if(!(so.targetObject is ScenarioData data))
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            string[] list = k_defaultList.Concat(
                    data.Commands
                    .Where(command => command is LabelCommand)
                    .Select(command => (command as LabelCommand).Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                ).ToArray();

            if (!list.Any())
            {
                EditorGUI.LabelField(position, label, new GUIContent("Labelを追加してください"));
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
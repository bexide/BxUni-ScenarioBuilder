using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [CustomPropertyDrawer(typeof(MonoScriptSelectorAttribute))]
    internal class MonoScriptSelectAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(fieldInfo.FieldType != typeof(MonoScript))
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            //Label
            var rect = new Rect(EditorGUI.IndentedRect(position))
            {
                width = EditorGUIUtility.labelWidth
            };
            GUI.Label(rect, property.displayName);

            //Button
            rect.xMin = rect.xMax - 10;
            rect.xMax = position.xMax;

            string name;
            try
            {
                var script = property.objectReferenceValue as MonoScript;
                var cls = script.GetClass();
                name = cls.FullName;
            }
            catch
            {
                name = "None (MonoScript)";
            }

            //選択時
            void OnSelected(MonoScript script)
            {
                var so = property.serializedObject;
                so.Update();

                //新しいScriptに切り替える
                property.objectReferenceValue = script;

                so.ApplyModifiedProperties();
            }

            var style = new GUIStyle(EditorStyles.popup)
            {
                fontStyle = FontStyle.Bold
            };

            if (GUI.Button(rect, name, style))
            {
                var dropdown = new MonoScriptSelectDropdown(OnSelected);
                dropdown.Show(rect);
            }
        }


    }
}
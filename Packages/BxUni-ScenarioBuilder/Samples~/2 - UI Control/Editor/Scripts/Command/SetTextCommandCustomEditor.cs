using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.UIControl.Editor
{
    [CustomCommandEditor(typeof(SetTextBoxCommand))]
    internal class SetTextCommandCustomEditor : CustomCommandEditor
    {
        SetTextBoxCommand Cmd
            => target as SetTextBoxCommand;

        public override void OnGUI(Rect rect)
        {
            string text = Cmd.Text;
            if(!string.IsNullOrEmpty(text) && text.Length >= 10)
            {
                text = $"{text.Substring(0, 10)}...";
            }
            EditorGUI.LabelField(rect, $"{text}");
        }

        public override bool HasPreviewArea()
        {
            return true;
        }

        public override void OnPreviewAreaGUI(Rect rect, SerializedProperty property)
        {
            var textProp = property.FindPropertyRelative("m_text");
            EditorGUI.PropertyField(rect, textProp);
        }

        public override float GetPreviewAreaHeight(SerializedProperty property)
        {
            var textProp = property.FindPropertyRelative("m_text");
            float propertyHeight = EditorGUI.GetPropertyHeight(textProp);
            float height = Mathf.Max(propertyHeight, base.GetPreviewAreaHeight(property));

            return height;
        }
    }
}
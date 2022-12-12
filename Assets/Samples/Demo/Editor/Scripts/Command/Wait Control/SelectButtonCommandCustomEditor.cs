using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomCommandEditor(typeof(SelectButtonCommand))]
    public class SelectButtonCommandCustomEditor : CustomCommandEditor
    {
        SelectButtonCommand Cmd
            => target as SelectButtonCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"選択肢数: {Cmd.Labels.Length}");
        }

        public override bool HasPreviewArea()
        {
            return true;
        }

        public override void OnPreviewAreaGUI(Rect rect, SerializedProperty property)
        {
            var sb = new StringBuilder();
            foreach(string label in Cmd.Labels)
            {
                sb.AppendLine($"* {label}");
            }
            EditorGUI.LabelField(rect, sb.ToString());
        }


    }
}
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomCommandEditor(typeof(PrefabSpawnSetupCommand))]
    internal class PrefabSpawnSetupCommandCustomEditor : CustomCommandEditor
    {
        PrefabSpawnSetupCommand Cmd
            => target as PrefabSpawnSetupCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"データ設定数: {Cmd.Setups.Length}");
        }

        public override bool HasPreviewArea()
        {
            return true;
        }

        public override float GetPreviewAreaHeight(SerializedProperty property)
        {
            int length = Cmd.Setups.Length;
            float height = EditorGUIUtility.singleLineHeight * length;

            return Mathf.Max(height, base.GetPreviewAreaHeight(property));
        }

        public override void OnPreviewAreaGUI(Rect rect, SerializedProperty property)
        {
            var sb = new StringBuilder();
            foreach(var setup in Cmd.Setups)
            {
                sb.AppendLine($"*{setup.ID}");
            }
            EditorGUI.LabelField(rect, sb.ToString());
        }

    }
}
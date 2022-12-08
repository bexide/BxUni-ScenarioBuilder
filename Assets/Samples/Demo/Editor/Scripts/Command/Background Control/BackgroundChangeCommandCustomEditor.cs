using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomCommandEditor(typeof(BackgroundChangeCommand))]
    public class BackgroundChangeCommandCustomEditor : CustomCommandEditor
    {
        BackgroundChangeCommand Cmd
            => target as BackgroundChangeCommand;

        public override void OnGUI(Rect rect)
        {
            var sprite = Cmd.BackgroundSprite;
            if(sprite == null)
            {
                base.OnGUI(rect);
                return;
            }

            string path = AssetDatabase.GetAssetPath(sprite);
            EditorGUI.LabelField(
                rect, $"BG: {Path.GetFileName(path)}");
        }

        public override bool HasPreviewArea()
        {
            return true;
        }

        public override void OnPreviewAreaGUI(Rect rect, SerializedProperty property)
        {
            var sprite = Cmd.BackgroundSprite;
            if(sprite != null)
            {
                EditorGUI.LabelField(rect, new GUIContent(sprite.texture));
            }
            else
            {
                EditorGUI.LabelField(rect, "Not Preview...");
            }
        }


    }
}
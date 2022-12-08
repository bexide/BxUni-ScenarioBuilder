using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomCommandEditor(typeof(SetTextCommand))]
    internal class SetTextCommandCustomEditor : CustomCommandEditor
    {
        SetTextCommand Cmd
            => target as SetTextCommand;

        public override void OnGUI(Rect rect)
        {
            string text = Cmd.Text;
            if(!string.IsNullOrEmpty(text) && text.Length >= 10)
            {
                text = $"{text.Substring(0, 10)}...";
            }
            EditorGUI.LabelField(rect, text);
        }

    }
}
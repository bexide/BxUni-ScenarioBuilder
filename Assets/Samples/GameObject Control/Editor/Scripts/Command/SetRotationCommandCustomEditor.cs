using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl.Editor
{
    [CustomCommandEditor(typeof(SetRotationCommand))]
    internal class SetRotationCommandCustomEditor : CustomCommandEditor
    {
        SetRotationCommand Cmd
            => target as SetRotationCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"SetRotation [{Cmd.ID}] - {Cmd.Rotation}");
        }

        public override bool Validate(out string errorLog)
        {
            errorLog = string.Empty;

            if (string.IsNullOrEmpty(Cmd.ID))
            {
                errorLog = "IDを入力してください";
            }

            //エラーログが空文字なら問題なし
            return string.IsNullOrEmpty(errorLog);
        }
    }
}
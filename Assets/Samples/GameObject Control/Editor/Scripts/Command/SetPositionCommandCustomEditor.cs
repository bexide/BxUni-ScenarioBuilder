using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl.Editor
{
    [CustomCommandEditor(typeof(SetPositionCommand))]
    internal class SetPositionCommandCustomEditor : CustomCommandEditor
    {
        SetPositionCommand Cmd
            => target as SetPositionCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"SetPosition [{Cmd.ID}] - {Cmd.Position}");
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

using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl.Editor
{
    [CustomCommandEditor(typeof(SetScaleCommand))]
    internal class SetScaleCommandCustomEditor : CustomCommandEditor
    {
        SetScaleCommand Cmd
            => target as SetScaleCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"SetScale [{Cmd.ID}] - {Cmd.Scale}");
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
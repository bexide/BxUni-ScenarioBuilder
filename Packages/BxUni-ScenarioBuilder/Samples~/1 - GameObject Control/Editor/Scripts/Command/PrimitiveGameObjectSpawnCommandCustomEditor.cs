using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl.Editor
{
    [CustomCommandEditor(typeof(PrimitiveGameObjectSpawnCommand))]
    internal class PrimitiveGameObjectSpawnCommandCustomEditor : CustomCommandEditor
    {
        PrimitiveGameObjectSpawnCommand Cmd
            => target as PrimitiveGameObjectSpawnCommand;
        
        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"Spawn [{Cmd.ID}] - {Cmd.PrimitiveType}");
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
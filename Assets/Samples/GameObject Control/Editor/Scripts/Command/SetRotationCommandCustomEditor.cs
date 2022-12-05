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
    }
}
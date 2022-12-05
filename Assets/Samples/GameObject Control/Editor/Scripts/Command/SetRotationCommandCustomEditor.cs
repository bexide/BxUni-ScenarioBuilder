using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.Sample.GameObjectControl.Editor
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
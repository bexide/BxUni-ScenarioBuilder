using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.Sample.GameObjectControl.Editor
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
    }
}
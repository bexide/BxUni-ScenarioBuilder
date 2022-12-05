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
    }
}

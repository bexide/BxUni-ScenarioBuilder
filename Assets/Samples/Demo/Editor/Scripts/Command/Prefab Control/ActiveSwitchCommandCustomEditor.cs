using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomCommandEditor(typeof(ActiveSwitchCommand))]
    internal class ActiveSwitchCommandCustomEditor : CustomCommandEditor
    {
        ActiveSwitchCommand Cmd
            => target as ActiveSwitchCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"{Cmd.ID}[{(Cmd.Active ? "✔" : "-")}] ");
        }
    }
}
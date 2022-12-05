using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.Editor
{
    [CustomCommandEditor(typeof(JumpCommand))]
    internal class JumpCommandCustomEditor : CustomCommandEditor
    {

        JumpCommand Target => target as JumpCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"To {Target.TargetLabel}");
        }

    }
}
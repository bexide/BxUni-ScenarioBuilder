//BeXide 2022-11-29
//by MurakamiKazuki

using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
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
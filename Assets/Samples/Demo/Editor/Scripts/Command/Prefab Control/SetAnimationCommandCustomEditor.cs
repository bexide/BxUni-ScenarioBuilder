using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomCommandEditor(typeof(SetAnimationCommand))]
    public class SetAnimationCommandCustomEditor : CustomCommandEditor
    {
        SetAnimationCommand Cmd => target as SetAnimationCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"{Cmd.ID}: {Cmd.AnimationName}");
        }
    }
}
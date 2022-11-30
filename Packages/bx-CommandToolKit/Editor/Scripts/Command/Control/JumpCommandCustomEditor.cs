using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.Editor
{
    [CustomCommandEditor(typeof(JumpCommand))]
    public class JumpCommandCustomEditor : CustomCommandEditor
    {

        JumpCommand Target => target as JumpCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"To {Target.TargetLabel}");
        }

    }
}
using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.Editor 
{
    [CustomCommandEditor(typeof(LabelCommand))]
    public class LabelCommandCustomEditor : CustomCommandEditor
    {

        LabelCommand Target => target as LabelCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"*{Target.Name}");
        }
        
    }
}
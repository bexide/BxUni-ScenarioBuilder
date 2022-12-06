using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.Editor 
{
    [CustomCommandEditor(typeof(LabelCommand))]
    internal class LabelCommandCustomEditor : CustomCommandEditor
    {

        LabelCommand Target => target as LabelCommand;

        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"*{Target.Name}");
        }
        
    }
}
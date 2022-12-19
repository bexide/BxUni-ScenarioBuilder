//BeXide 2022-11-29
//by MurakamiKazuki

using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
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
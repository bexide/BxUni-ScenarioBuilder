using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
{
    [CustomEditor(typeof(ScenarioData))]
    internal class ScenarioDataInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Open Editor"))
            {
                ScenarioEditFlowWindow.CreateWindow(target as ScenarioData);
            }
            base.OnInspectorGUI();
        }
    }
}
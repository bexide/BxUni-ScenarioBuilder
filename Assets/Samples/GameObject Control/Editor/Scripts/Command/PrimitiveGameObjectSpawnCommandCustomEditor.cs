using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl.Editor
{
    [CustomCommandEditor(typeof(PrimitiveGameObjectSpawnCommand))]
    internal class PrimitiveGameObjectSpawnCommandCustomEditor : CustomCommandEditor
    {
        PrimitiveGameObjectSpawnCommand Cmd
            => target as PrimitiveGameObjectSpawnCommand;
        
        public override void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(
                rect, $"Spawn [{Cmd.ID}] - {Cmd.PrimitiveType}");
        }

    }
}
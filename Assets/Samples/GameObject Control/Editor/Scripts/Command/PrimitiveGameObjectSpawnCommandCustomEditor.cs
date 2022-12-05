using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.Sample.GameObjectControl.Editor
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
using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// 画面上部のファイル情報のエリア
    /// </summary>
    internal class ScenarioFileInfoArea
    {

        internal void DrawLayout(ScenarioData data)
        {
            using var _ = new BackgroundColorScope(EditorGUIProperty.FileInfoAreaBackgroundColor);
            using var __ = new EditorGUILayout.HorizontalScope(GUI.skin.box);

            string path = AssetDatabase.GetAssetPath(data);
            EditorGUILayout.LabelField($"Edit : {path}");

            using(new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.ObjectField(data, typeof(ScenarioData), allowSceneObjects: false);
            }
        }

    }
}
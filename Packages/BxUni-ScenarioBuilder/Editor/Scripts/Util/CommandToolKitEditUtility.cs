using System.Text;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal 
{
    internal static class ScenarioBuilderEditUtility
    {
        #region const

        static readonly string k_saveDirectoryPathKey = $"ScenarioBuilder.{PlayerSettings.productGUID}.SaveKey";

        #endregion

        #region property

        internal static BaseCommand[] ClipBoardCommands { get; set; } = new BaseCommand[0];

        #endregion

        internal static void PasetAsNew()
        {
            var clipboards = ClipBoardCommands;
            if (clipboards == null) { return; }

            var editData = ScenarioEditFlowWindow.Instance?.CurrentEditData;
            if (editData == null) { return; }

            foreach(var clipboard in clipboards)
            {
                editData.Commands.Add(clipboard.Clone());
            }
            EditorUtility.SetDirty(editData);
        }

        #region Menu Function

        [ScenarioBuilderEditorMenuItem("File/New Scenario", 99999999)]
        internal static void CreateAsset()
        {
            //最後に開いたファイルのディレクトリパスを取得
            //無ければApplication.dataPathを指定
            string directoryPath = EditorPrefs.GetString(k_saveDirectoryPathKey, Application.dataPath);
            if (!Directory.Exists(directoryPath))
            {
                directoryPath = Application.dataPath;
            }

            string selectPath = EditorUtility.SaveFilePanel(
                "新規作成", directoryPath, "New Scenario", "asset");
            string path = selectPath.Replace(Application.dataPath, "Assets");

            if (string.IsNullOrEmpty(path)) { return; }

            var newObj = ScriptableObject.CreateInstance<ScenarioData>();
            AssetDatabase.CreateAsset(newObj, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var asset = AssetDatabase.LoadAssetAtPath<ScenarioData>(path);
            ScenarioEditFlowWindow.CreateWindow(asset);

            string saveDirectoryPath = Path.GetDirectoryName(selectPath);
            EditorPrefs.SetString(k_saveDirectoryPathKey, saveDirectoryPath);
        }

        [ScenarioBuilderEditorMenuItem("File/Open Scenario", 99999998)]
        internal static void OpenAsset()
        {
            //最後に開いたファイルのディレクトリパスを取得
            //無ければApplication.dataPathを指定
            string directoryPath = EditorPrefs.GetString(k_saveDirectoryPathKey, Application.dataPath);
            if (!Directory.Exists(directoryPath))
            {
                directoryPath = Application.dataPath;
            }

            string selectPath = EditorUtility.OpenFilePanel(
                "開く", directoryPath, "asset");
            string path = selectPath.Replace(Application.dataPath, "Assets");

            if (string.IsNullOrEmpty(path)) { return; }

            var asset = AssetDatabase.LoadAssetAtPath<ScenarioData>(path);
            if(asset == null)
            {
                EditorUtility.DisplayDialog(
                    title  : "Error",
                    message: "形式の違うファイルです",
                    ok     : "OK");
            }
            else
            {
                ScenarioEditFlowWindow.CreateWindow(asset);

                string saveDirectoryPath = Path.GetDirectoryName(selectPath);
                EditorPrefs.SetString(k_saveDirectoryPathKey, saveDirectoryPath);
            }
        }

        [ScenarioBuilderEditorMenuItem("File/Export/Json", 99999989)]
        internal static void ExportJson()
        {
            var scenario = ScenarioEditFlowWindow.Instance?.CurrentEditData;
            if (scenario == null)
            {
                Debug.LogError("Scenario data not set.");
                return;
            }

            string path = EditorUtility.SaveFilePanel(
                "Export File...", Application.dataPath, $"{scenario.name}", "json");
            if (string.IsNullOrEmpty(path)) { return; }

            string json = EditorJsonUtility.ToJson(scenario, prettyPrint: true);
            File.WriteAllText(path, json);

            AssetDatabase.Refresh();
            Debug.Log(json);
        }

        [ScenarioBuilderEditorMenuItem("File/Export/TSV", 99999988)]
        internal static void ExportCsv()
        {
            var scenario = ScenarioEditFlowWindow.Instance?.CurrentEditData;
            if (scenario == null)
            {
                Debug.LogError("Scenario data not set.");
                return;
            }

            string path = EditorUtility.SaveFilePanel(
                "Export File...", Application.dataPath, $"{scenario.name}", "tsv");
            if (string.IsNullOrEmpty(path)) { return; }

            var sb = new StringBuilder();
            var so = new SerializedObject(scenario);
            var listProperty = so.FindProperty("m_commands");

            var allDrawer = CommandRegistConfig.GetAllCommandGroup().SelectMany(x => x.CommandDrawers);

            while (listProperty.NextVisible(true))
            {
                var prop = listProperty;
                if(prop.depth > 2)
                {
                    continue;
                }

                if (prop.propertyType == SerializedPropertyType.ArraySize)
                {
                    continue;
                }
                if(prop.propertyType == SerializedPropertyType.ManagedReference)
                {
                    if(sb.Length > 0)
                    {
                        sb.AppendLine();
                    }

                    var type = prop.managedReferenceValue.GetType();
                    var drawer = allDrawer.FirstOrDefault(x => x.GetClassType() == type);
                    if(drawer == null)
                    {
                        sb.Append($"{type.Name}\t");
                    }
                    else
                    {
                        sb.Append($"{drawer.ViewNameText}\t");
                    }

                    continue;
                }

                string value = prop.propertyType switch
                {
                    SerializedPropertyType.Integer          => $"{prop.intValue}",
                    SerializedPropertyType.Float            => $"{prop.floatValue}",
                    SerializedPropertyType.Boolean          => $"{prop.boolValue}",
                    SerializedPropertyType.String           => $"{prop.stringValue}",
                    SerializedPropertyType.Color            => $"{prop.colorValue}",
                    SerializedPropertyType.ObjectReference  => $"{prop.objectReferenceValue?.name ?? "None"}",
                    SerializedPropertyType.Enum             => $"{prop.enumNames[prop.enumValueIndex]}",
                    SerializedPropertyType.Vector2          => $"{prop.vector2Value}",
                    SerializedPropertyType.Vector3          => $"{prop.vector3Value}",
                    SerializedPropertyType.Vector4          => $"{prop.vector4Value}",
                    SerializedPropertyType.Rect             => $"{prop.rectValue}",
                    SerializedPropertyType.AnimationCurve   => $"{prop.animationCurveValue.keys}",
                    SerializedPropertyType.Bounds           => $"{prop.boundsValue}",
                    SerializedPropertyType.Quaternion       => $"{prop.quaternionValue}",
                    SerializedPropertyType.ExposedReference => $"{prop.exposedReferenceValue?.name ?? "None"}",
                    SerializedPropertyType.Vector2Int       => $"{prop.vector2IntValue}",
                    SerializedPropertyType.Vector3Int       => $"{prop.vector3IntValue}",
                    SerializedPropertyType.RectInt          => $"{prop.rectIntValue}",
                    SerializedPropertyType.BoundsInt        => $"{prop.boundsIntValue}",
                    SerializedPropertyType.Hash128          => $"{prop.hash128Value}",
                    SerializedPropertyType.Character        => $"{(char)prop.intValue}",
                    SerializedPropertyType.LayerMask        => $"{prop.intValue}",
                    
                    _ => "export type mismatch",
                };

                sb.Append($"{value.Replace("\n","\\n")}\t");
            }

            File.WriteAllText(path, sb.ToString());
            AssetDatabase.Refresh();

            Debug.Log($"<color=yellow>{sb}</color>");
        }

        #endregion
    }
}
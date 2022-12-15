using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.Sample.EditorWindowMenuAdd.Editor
{
    public class SampleMenuButtonData
    {
        [ScenarioBuilderEditorMenuItem("Sample/Log")]
        public static void SampleLog()
        {
            Debug.Log("Sample Log");
        }

        [ScenarioBuilderEditorMenuItem("Sample/Generate Scenario")]
        public static void GenerateScenario()
        {
            //保存先の決定
            string saveFilePath = EditorUtility.SaveFilePanelInProject("シナリオの保存先", "", "asset", "Generate Scenario File");
            if (string.IsNullOrEmpty(saveFilePath))
            {
                Debug.LogWarning("Export failed.");
                return;
            }
            Debug.Log($"Export to {saveFilePath}");

            GenerateScenarioImpl(saveFilePath);
        }

        static void GenerateScenarioImpl(string saveFilePath)
        {
            var scenario = ScriptableObject.CreateInstance<ScenarioData>();

            /*
             * 生成したScenarioDataから「Commands」というリストにアクセスすることで
             * 任意のコマンドを追加した状態で生成することが可能です。
             * 
             * 例）
             * scenario.Commands.Add(new ExampleCommand());
             */

            AssetDatabase.CreateAsset(scenario, saveFilePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
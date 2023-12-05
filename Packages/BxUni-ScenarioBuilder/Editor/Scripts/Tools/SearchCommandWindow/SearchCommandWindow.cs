//BeXide 2023-12-04
//By MurakamiKazuki
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// プロジェクト内のシナリオファイルの中から指定したコマンドが含まれているファイルを検索するウィンドウ。
    /// </summary>
    internal class SearchCommandWindow : EditorWindow
    {
        class LogEntry
        {
            public ScenarioData Scenario { get; set; }

            public string[] Logs { get; set; }
        }

        GUIStyle m_labelStyle;
        MonoScript m_cacheMonoScript = null;
        Vector2 m_scroll;
        List<LogEntry> m_logEntlies = new();

        [MenuItem("BeXide/ScenarioBuilder/Tools/SearchCommandWindow", priority = 100)]
        [ScenarioBuilderEditorMenuItem("Tools/SearchCommandWindow", 89999999)]
        internal static void Open()
        {
            var window = GetWindow<SearchCommandWindow>(nameof(SearchCommandWindow));
        }

        void OnGUI()
        {
            // チェックするコマンドを検索するUIの表示
            var rect = GUILayoutUtility.GetRect(position.width, height: 20);
            rect.xMin = 5;
            rect.xMax = rect.xMax - 5;

            string label;
            try
            {
                label = m_cacheMonoScript?.GetClass().FullName ?? string.Empty;
            }
            catch
            {
                label             = string.Empty;
                m_cacheMonoScript = null;
            }
            MonoScriptSelectDropdownDrawer.Draw(
                rect,
                label,
                targetCommand => m_cacheMonoScript = targetCommand);

            HandleDrawUtility.DrawRectBox(rect, Color.green);

            if(m_cacheMonoScript == null)
            {
                EditorGUILayout.HelpBox("↑検索したいコマンドを指定してください", MessageType.Warning);
                return;
            }

            EditorGUILayout.HelpBox($"[{label}]がシナリオファイルに含まれているコマンドを表示します", MessageType.Info);

            if (GUILayout.Button("チェック"))
            {
                var targetType = m_cacheMonoScript.GetClass();
                Check(targetType);
            }

            DrawLogEntries();
        }

        void Check(System.Type targetType)
        {
            m_logEntlies.Clear();

            var targetTypeIncludeScenarios = AssetDatabase
                .FindAssets($"t:{nameof(ScenarioData)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScenarioData>)
                .Where(data => data.Commands.Any(cmd => cmd.GetType() == targetType));

            foreach (var scenario in targetTypeIncludeScenarios)
            {
                var list = new List<string>();
                var commands = scenario.Commands;
                for (int i = 0; i < commands.Count; i++)
                {
                    var cmd = commands[i];
                    if (cmd.GetType() != targetType) { continue; }

                    list.Add($"<color=gray>[{i}]</color><color=lime>{JsonUtility.ToJson(cmd)}</color>");
                }

                m_logEntlies.Add(new()
                {
                    Scenario = scenario,
                    Logs = list.ToArray(),
                });
            }

        }

        void DrawLogEntries()
        {
            if (m_logEntlies.Count == 0) { return; }

            m_labelStyle = new GUIStyle()
            {
                richText = true,
            };

            using var scroll = new EditorGUILayout.ScrollViewScope(m_scroll);
            using var _ = new EditorGUI.IndentLevelScope(1);

            foreach (var logEntry in m_logEntlies)
            {
                EditorGUILayout.ObjectField(logEntry.Scenario, typeof(ScenarioData), false);

                using var __ = new EditorGUI.IndentLevelScope(1);
                foreach (string log in logEntry.Logs)
                {
                    EditorGUILayout.SelectableLabel(log, m_labelStyle);
                }
            }
            m_scroll = scroll.scrollPosition;
        }

    }
}

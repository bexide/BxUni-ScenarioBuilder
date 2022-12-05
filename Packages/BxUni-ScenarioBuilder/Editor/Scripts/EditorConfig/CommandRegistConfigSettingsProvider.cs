using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class CommandRegistConfigSettingsProvider : SettingsProvider
    {
        const string k_projectSettingPath = "BX/ScenarioBuilder/CommandRegistConfig";

        Vector2 m_scrollPos = Vector2.zero;
        SerializedObject[] m_configs;

        public CommandRegistConfigSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            m_configs = AssetDatabase.FindAssets($"t:{nameof(CommandRegistConfig)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CommandRegistConfig>)
                .Select(config => new SerializedObject(config))
                .ToArray();
        }

        public override void OnGUI(string searchContext)
        {
            if(m_configs.Length <= 0)
            {
                EditorGUILayout.HelpBox("1つ以上設定を追加してください。", MessageType.Warning);
                return;
            }

            using var scroll = new GUILayout.ScrollViewScope(m_scrollPos);
            foreach (var config in m_configs)
            {
                config.Update();

                using var change = new EditorGUI.ChangeCheckScope();
                EditorGUILayout.LabelField($"{config.targetObject.name}", GUI.skin.box);
                
                var rect = GUILayoutUtility.GetLastRect();
                HandleDrawUtility.DrawRectBox(rect, Color.green);

                EditorGUILayout.PropertyField(config.FindProperty("m_commandDrawerGroup"));

                if (change.changed)
                {
                    config.ApplyModifiedProperties();
                }
            }
            m_scrollPos = scroll.scrollPosition;
        }

        [SettingsProvider]
        static SettingsProvider ProjectSettingsGUI()
        {
            var provider = new CommandRegistConfigSettingsProvider(k_projectSettingPath, SettingsScope.Project);
            return provider;
        }

        [ScenarioBuilderEditorMenuItem("Help/ProjectSettings/RegistSettings", 79999999)]
        static void OpenScenarioBuilderRegistSettings()
        {
            SettingsService.OpenProjectSettings(k_projectSettingPath);
        }
    }
}
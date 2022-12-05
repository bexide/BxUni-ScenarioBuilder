using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{

    internal class ScenarioBuilderEditorConfigSettingProvider : SettingsProvider
    {
        const string k_projectSettingPath = "BX/ScenarioBuilder/EditorConfig";

        SerializedObject m_serializeObject;

        internal ScenarioBuilderEditorConfigSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var config = ScenarioBuilderEditorConfig.FindConfig();
            m_serializeObject = new SerializedObject(config);
        }

        public override void OnGUI(string searchContext)
        {
            var prop = m_serializeObject.FindProperty("m_helpUrl");
            m_serializeObject.Update();

            using var change = new EditorGUI.ChangeCheckScope();

            EditorGUILayout.PropertyField(prop);

            if(change.changed)
            {
                m_serializeObject.ApplyModifiedProperties();
            }
        }

        [SettingsProvider]
        static SettingsProvider ProjectSettingsGUI()
        {
            var provider = new ScenarioBuilderEditorConfigSettingProvider(k_projectSettingPath, SettingsScope.Project);
            return provider;
        }

        [ScenarioBuilderEditorMenuItem("Help/ProjectSettings/EditorConfig", 79999998)]
        static void OpenScenarioBuilderEditorConfig()
        {
            SettingsService.OpenProjectSettings(k_projectSettingPath);
        }
    }
}
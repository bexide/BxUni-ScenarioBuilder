using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.EditorInternal
{

    internal class CommandToolKitEditorConfigSettingProvider : SettingsProvider
    {
        const string k_projectSettingPath = "BX/CommandToolKit/EditorConfig";

        SerializedObject m_serializeObject;

        internal CommandToolKitEditorConfigSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
            : base(path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var config = CommandToolKitEditorConfig.FindConfig();
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
            var provider = new CommandToolKitEditorConfigSettingProvider(k_projectSettingPath, SettingsScope.Project);
            return provider;
        }

        [CommandToolKitEditorMenuItem("Help/ProjectSettings/EditorConfig", 79999998)]
        static void OpenCommandToolKitEditorConfig()
        {
            SettingsService.OpenProjectSettings(k_projectSettingPath);
        }
    }
}
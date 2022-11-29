using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.EditorInternal
{
    internal class CommandToolKitEditorConfig : ScriptableObject
    {
#pragma warning disable 0649
        [Header("ヘルプページを開くためのURL")]
        [SerializeField] string m_helpUrl = "";
#pragma warning restore 0649

        /// <summary>
        /// ヘルプページのURL
        /// </summary>
        internal string HelpUrl => m_helpUrl;

        internal static CommandToolKitEditorConfig FindConfig()
        {
            var asset = AssetDatabase.FindAssets($"t:{nameof(CommandToolKitEditorConfig)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CommandToolKitEditorConfig>)
                .FirstOrDefault(x => x != null);
            if(asset == null)
            {
                asset = CreateInstance<CommandToolKitEditorConfig>();

                string folderPath = "Assets/BX/Editor/BX-CommandToolKit";

                var split = folderPath.Split('/');
                var sb = new StringBuilder(split[0]);
                foreach (string folder in split.Skip(1))
                {
                    if (!AssetDatabase.IsValidFolder($"{sb}/{folder}"))
                    {
                        AssetDatabase.CreateFolder(sb.ToString(), folder);
                        sb.Append($"/{folder}");
                    }
                }

                string path = $"{folderPath}/{nameof(CommandToolKitEditorConfig)}.asset";
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            return asset;
        }

        [CommandToolKitEditorMenuItem("Help/Go to Help")]
        internal static void GotoHelpPage()
        {
            var config = FindConfig();
            string url = config?.HelpUrl ?? string.Empty;
            if(!string.IsNullOrEmpty(url))
            {
                Application.OpenURL(url);
            }
        }
    }
}
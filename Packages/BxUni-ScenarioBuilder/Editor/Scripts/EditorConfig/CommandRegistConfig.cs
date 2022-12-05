using System.Linq;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [CreateAssetMenu(menuName = k_menuName)]
    internal class CommandRegistConfig : ScriptableObject
    {

        const string k_menuName = "BeXide/ScenarioBuilder/CommandRegistConfig";

#pragma warning disable 0649
        [SerializeField] CommandDrawerGroup[] m_commandDrawerGroup;
#pragma warning restore 0649

        internal static CommandDrawerGroup[] GetAllCommandGroup()
        {
            if(s_allCommandGroup == null)
            {
                s_allCommandGroup = AssetDatabase.FindAssets($"t:{nameof(CommandRegistConfig)}")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<CommandRegistConfig>)
                    .Where(x => x != null)
                    .Reverse()
                    .SelectMany(x => x.m_commandDrawerGroup)
                    .ToArray();
            }
            return s_allCommandGroup;
        }
        static CommandDrawerGroup[] s_allCommandGroup;

        [ScenarioBuilderEditorMenuItem("Edit/Reset Window", 89999999)]
        internal static void ResetCache()
        {
            s_allCommandGroup = null;
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal static class EditorGUIIcons
    {
        internal static readonly Vector2 IconDefaultSize
            = new Vector2(32, 32);

        /// <summary>
        /// GUILayout系のアイコンサイズ
        /// </summary>
        internal static GUILayoutOption[] IconSizeLayoutOption(float size)
        {
            return IconSizeLayoutOption(size, size);
        }

        internal static GUILayoutOption[] IconSizeLayoutOption(Vector2 size)
        {
            return IconSizeLayoutOption(size.x, size.y);
        }

        internal static GUILayoutOption[] IconSizeLayoutOption(float width, float height)
        {
            return new GUILayoutOption[]
            {
                GUILayout.Width(width),
                GUILayout.Height(height)
            };
        }

        internal static readonly GUIContent DuplicateButtonTex
            = Create("TreeEditor.Duplicate", "このコマンドを複製します。");

        internal static readonly GUIContent RemoveButtonTex
            = Create("TreeEditor.Trash", "このコマンドを削除します。");

        internal static readonly GUIContent HelpIconTex
            = Create("_Help");

        internal static readonly GUIContent DefaultCommandIcon
            = Create("TextAsset Icon");

        internal static readonly GUIContent OpenButtonTex
            = Create("SocialNetworks.UDNOpen");

        internal static readonly GUIContent ErrorIconTex
            = Create("console.erroricon");

        static GUIContent Create(string name, string tooltip = "")
        {
            var icon = EditorGUIUtility.IconContent(name);
            return new GUIContent(icon)
            {
                tooltip = tooltip,
            };
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
{
    internal class CommandHelpArea
    {
        #region const

        const float k_iconSize = 20f;

        #endregion

        internal void DrawLayout()
        {
            using var _ = new EditorGUILayout.HorizontalScope(GUI.skin.box);

            DrawIconLayout();
            GUILayout.Label(CommandDrawer.TooltipMessage);
        }

        void DrawIconLayout()
        {
            using var _ = new ContentColorScope(Color.green);
            GUILayout.Label(EditorGUIIcons.HelpIconTex,
                EditorGUIIcons.IconSizeLayoutOption(k_iconSize));
        }

    }
}
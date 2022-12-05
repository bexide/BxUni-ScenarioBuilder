using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
{
    internal class Foldout
    {
        #region Property

        bool IsDisplay { get; set; }

        #endregion

        internal Foldout(bool isDisplay = false)
        {
            IsDisplay = isDisplay;
        }

        static GUIStyle GetStyle()
        {
            return new GUIStyle("ShurikenModuleTitle")
            {
                font          = new GUIStyle(EditorStyles.label).font,
                fontStyle     = FontStyle.Bold,
                border        = new RectOffset(15, 7, 4, 4),
                fixedHeight   = 22,
                contentOffset = new Vector2(20f, -2f),
            };
        }

        #region DrawGUI

        internal bool Draw(Rect rect, string title, Color color)
        {
            var style = GetStyle();
            using (new BackgroundColorScope(color))
            {
                GUI.Box(rect, title, style);
            }
            return DrawImpl(rect);
        }

        bool DrawImpl(Rect rect)
        {
            var ev = Event.current;
            if (ev.type == EventType.Repaint)
            {
                var toggleRect = new Rect()
                {
                    x      = rect.x + 4f,
                    y      = rect.y + 2f,
                    width  = 13f,
                    height = 13f,
                };
                EditorStyles.foldout.Draw(toggleRect, false, false, IsDisplay, false);
            }

            if (ev.type == EventType.MouseDown && rect.Contains(ev.mousePosition))
            {
                IsDisplay = !IsDisplay;
                ev.Use();
            }

            return IsDisplay;
        }

        #endregion

        #region DrawGUILayout

        internal bool DrawLayout(string title, Color color)
        {
            var style = GetStyle();

            using (new BackgroundColorScope(color))
            {
                GUILayout.Box(title, style);
            }

            var rect = GUILayoutUtility.GetLastRect();
            return DrawImpl(rect);
        }

        #endregion
    }
}
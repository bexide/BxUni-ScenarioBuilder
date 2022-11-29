using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.EditorInternal
{
    /// <summary>
    /// 画面中央右側の情報表示部分
    /// </summary>
    internal class CommandInspectorArea
    {

        #region property

        Vector2 m_scrollPos = Vector2.zero;

        #endregion

        internal void DrawLayout(ScenarioData scenario, SerializedObject so, IReadOnlyCollection<int> selectedIndicators)
        {
            using var _ = new GUILayout.VerticalScope(GUI.skin.box, GUILayout.ExpandHeight(true));
            using var scroll = new GUILayout.ScrollViewScope(m_scrollPos, GUI.skin.box);

            if (!selectedIndicators.Any())
            {
                EditorGUILayout.HelpBox("コンポーネントを選択してください", MessageType.Info);
            }
            else
            {
                var commandsProp = so.FindProperty("m_commands");
                (BaseCommand, SerializedProperty, CommandDrawer)[] selected = selectedIndicators
                    .Select(i =>
                    {
                        var sp        = commandsProp.GetArrayElementAtIndex(i);
                        var command   = scenario.Commands[i];
                        var drawer    = command.FindDrawer();
                        return (command, sp, drawer);
                    }).ToArray();
                foreach(var (command, prop, drawer) in selected)
                {
                    DrawLayoutImpl(command, prop, drawer);
                }
            }

            m_scrollPos = scroll.scrollPosition;
        }

        internal void DrawLayoutImpl(BaseCommand command, SerializedProperty property, CommandDrawer drawer)
        {
            drawer.DrawLayout(GUILayout.ExpandWidth(true));

            using (new EditorGUI.IndentLevelScope(-1))
            {
                property.isExpanded = true;
                EditorGUILayout.PropertyField(property, GUIContent.none, true);

                var ev = Event.current;
                if (ev.keyCode == (KeyCode.Return & (KeyCode.LeftControl | KeyCode.RightControl)))
                {
                    GUI.FocusControl(string.Empty);
                }
            }

            if (!CommandValidator.Validate(command, out string errorLog))
            {
                EditorGUILayout.HelpBox(errorLog, MessageType.Error);

                var lastRect = GUILayoutUtility.GetLastRect();
                HandleDrawUtility.DrawRectBox(lastRect, Color.red);
            }

            var rect = GUILayoutUtility.GetLastRect();
            rect.y      = rect.yMax + 1;
            rect.height = 1;
            HandleDrawUtility.DrawRectBox(rect, Color.white);
        }
    }
}
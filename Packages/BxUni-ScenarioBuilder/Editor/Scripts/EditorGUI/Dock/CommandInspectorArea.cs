//BeXide 2022-11-29
//by MurakamiKazuki

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// 画面中央右側の情報表示部分
    /// </summary>
    internal class CommandInspectorArea
    {

        #region property

        Vector2 m_scrollPos = Vector2.zero;

        Rect m_latestRect = new Rect();

        public Rect WindowsSize { get; set; }
        #endregion

        internal void DrawLayout(ScenarioData scenario, SerializedObject so, IReadOnlyCollection<int> selectedIndicators)
        {
            using (var scroll = new GUILayout.ScrollViewScope(
                m_scrollPos,
                GUI.skin.box,
                GUILayout.MinHeight(WindowsSize.height)))
            {
                m_scrollPos = scroll.scrollPosition;
                DrawLayoutImpl(scenario, so, selectedIndicators);
            }

            if(Event.current.type == EventType.Repaint)
            {
                var rect = GUILayoutUtility.GetLastRect();
                if (rect != Rect.zero)
                {
                    m_latestRect = rect;
                    m_latestRect.yMin = WindowsSize.yMin;
                    m_latestRect.yMax = WindowsSize.yMax;
                }
            }

            HandleDrawUtility.DrawRectBox(m_latestRect, Color.white);
        }

        void DrawLayoutImpl(ScenarioData scenario, SerializedObject so, IReadOnlyCollection<int> selectedIndicators)
        {
            if (!selectedIndicators.Any())
            {
                EditorGUILayout.HelpBox("コンポーネントを選択してください", MessageType.Info);
            }
            else
            {
                var elementRect = new Rect()
                {
                    x      = m_latestRect.x,
                    width  = m_latestRect.width,
                    y      = m_latestRect.y,
                    height = 0,
                };
                var drawRect = new Rect(m_latestRect)
                {
                    y = m_latestRect.y + m_scrollPos.y
                };
                var commandsProp = so.FindProperty("m_commands");
                for(int i=0; i<selectedIndicators.Count; i++)
                {
                    elementRect.y += elementRect.height;

                    int index = selectedIndicators.ElementAtOrDefault(i);

                    var   prop   = commandsProp.GetArrayElementAtIndex(index);
                    elementRect.height = EditorGUI.GetPropertyHeight(prop);

                    if (!drawRect.Overlaps(elementRect, true))
                    {
                        GUILayout.Space(elementRect.height);
                        continue;
                    }

                    var command = scenario.Commands[index];
                    var drawer = command.FindDrawer();
                    _ = DrawLayoutElement(command, prop, drawer);
                }
            }
        }

        Rect DrawLayoutElement(BaseCommand command, SerializedProperty property, CommandDrawer drawer)
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
            return rect;
        }
    }
}
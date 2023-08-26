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

        #endregion

        internal void DrawLayout(ScenarioData scenario, SerializedObject so, IReadOnlyCollection<int> selectedIndicators)
        {
            using (var scroll = new GUILayout.ScrollViewScope(m_scrollPos, GUI.skin.box))
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
                }
            }
        }

        void DrawLayoutImpl(ScenarioData scenario, SerializedObject so, IReadOnlyCollection<int> selectedIndicators)
        {
            if (!selectedIndicators.Any())
            {
                EditorGUILayout.HelpBox("コンポーネントを選択してください", MessageType.Info);
            }
            else
            {
                float totalHeight = 0f;
                var commandsProp = so.FindProperty("m_commands");
                for(int i=0; i<selectedIndicators.Count; i++)
                {
                    int index = selectedIndicators.ElementAtOrDefault(i);

                    var   prop   = commandsProp.GetArrayElementAtIndex(index);
                    float height = EditorGUI.GetPropertyHeight(prop);

                    totalHeight += height;
                    if (IsRangeOver(totalHeight))
                    {
                        GUILayout.Space(height);
                        continue;
                    }

                    var command = scenario.Commands[index];
                    var drawer = command.FindDrawer();
                    DrawLayoutElement(command, prop, drawer);
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
    
        bool IsRangeOver(float yMax)
        {
            float min = m_scrollPos.y;
            float max = min + m_latestRect.yMax;
            return yMax <= min || max < yMax;
        }
    }
}
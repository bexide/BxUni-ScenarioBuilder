using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// Edit画面起動時の表示
    /// </summary>
    internal class InitializeArea
    {
        ScenarioTable m_table;

        internal void Initialize()
        {
            var state = new TreeViewState();
            var header = new ScenarioTableHeader();

            m_table = new ScenarioTable(state, header);       
        }

        internal void DrawLayout()
        {
            if (m_table == null)
            {
                Initialize();
            }

            if (m_table.AnyItems())
            {
                using var h1 = new HorizontalCenterScope(GUILayout.Width(300));
                using (var v1 = new VerticalCenterScope())
                {
                    if (GUILayout.Button("新規作成", GUILayout.Width(200)))
                    {
                        ScenarioBuilderEditUtility.CreateAsset();
                    }
                }

                var rect = GUILayoutUtility.GetLastRect();

                using var h2 = new HorizontalCenterScope();
                using var v2 = new VerticalCenterScope();

                var window = ScenarioEditFlowWindow.Instance.position;


                var areaRect = new Rect()
                {
                    x = rect.width + 30,
                    y = rect.y,
                    width = window.width - rect.width - 40,
                    height = rect.height,
                };
                m_table?.OnGUI(areaRect);
                HandleDrawUtility.DrawRectBox(areaRect, Color.white);
            }
            else
            {
                using var h1 = new HorizontalCenterScope();
                using (var v1 = new VerticalCenterScope())
                {
                    if (GUILayout.Button("新規作成", GUILayout.Width(200)))
                    {
                        ScenarioBuilderEditUtility.CreateAsset();
                    }
                }
            }

        }

        internal void Reset()
        {
            m_table?.Reload();
        }
    }
}
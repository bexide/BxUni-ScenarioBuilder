//BeXide 2022-12-12
//by MurakamiKazuki

using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// Edit画面起動時の表示
    /// </summary>
    internal class InitializeArea
    {
        static readonly string k_searchStringStateKey = $"{PlayerSettings.productGUID}_searchStringState";

        SearchField m_searchField;
        ScenarioTable m_table;

        internal void Initialize()
        {
            var state = new TreeViewState();
            var header = new ScenarioTableHeader();

            m_table = new ScenarioTable(state, header)
            {
                searchString = SessionState.GetString(k_searchStringStateKey, string.Empty)
            };

            m_searchField = new SearchField();
            m_searchField.downOrUpArrowKeyPressed
                += m_table.SetFocusAndEnsureSelectedItem;
        }

        internal void DrawLayout()
        {
            if (m_table == null)
            {
                Initialize();
            }

            if (m_table.AnyItems())
            {
                using var v1 = new EditorGUILayout.VerticalScope();
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();

                    EditorGUILayout.LabelField("Project Search:", GUILayout.Width(90));
                    using (var checkScope = new EditorGUI.ChangeCheckScope())
                    {
                        string search = m_searchField.OnToolbarGUI(m_table.searchString);
                        if (checkScope.changed)
                        {
                            SessionState.SetString(k_searchStringStateKey, search);
                            m_table.searchString = search;
                        }
                    }

                    using var _ = new BackgroundColorScope(Color.cyan);
                    if (GUILayout.Button("新規作成", GUILayout.Width(200)))
                    {
                        ScenarioBuilderEditUtility.CreateAsset();
                    }
                }

                using var v2 = new VerticalCenterScope();

                var window = ScenarioEditFlowWindow.Instance.position;

                float height = EditorGUIUtility.singleLineHeight * 2;
                var areaRect = new Rect()
                {
                    xMin = 5,
                    xMax = window.width - 5,

                    yMin = height + 10,
                    yMax = window.height - 32,
                };
                m_table?.OnGUI(areaRect);
                HandleDrawUtility.DrawRectBox(areaRect, Color.white);
            }
            else //Project内にScenarioが1つもない時
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
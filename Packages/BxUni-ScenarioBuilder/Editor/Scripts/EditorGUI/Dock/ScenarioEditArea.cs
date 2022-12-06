using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    /// <summary>
    /// 画面中央のコマンドが並ぶエリア
    /// </summary>
    internal class ScenarioEditArea
    {

        #region property

        internal IReadOnlyCollection<int> SelectedIndices => List?.selectedIndices.ToArray() ?? new int[0];

        internal int SelectedElementCount => SelectedIndices.Count;

        Vector2 m_scrollPos = Vector2.zero;

        ReorderableList List { get; set; } = null;

        internal ReSizeCalculator ResizeWidth { get; private set; }

        GUIStyle m_buttonStyle;

        #endregion

        internal void DrawLayout(ScenarioData data, SerializedObject so)
        {
            var window = ScenarioEditFlowWindow.Instance;
            if (window == null)
            {
                return;
            }
            var selectArea = window.SelectArea.ResizeWidth;

            if (ResizeWidth == null)
            {
                ResizeWidth = new ReSizeCalculator(500, ReSizeCalculator.PositionType.right, selectArea);
            }
            ResizeWidth.SetupMinMax(300, 800);

            #region ReorderableList_Callbacks

            void DrawHeader(Rect rect)
            {
                GUI.Label(rect, data.name);
            }

            void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
            {
                if (index < 0 || data.Commands.Count <= index) { return; }

                var command = data.Commands[index];
                var drawer  = command.FindDrawer();

                var elementRect = new Rect(rect)
                {
                    height = EditorGUIProperty.ElementHeight,
                };

                if (drawer == null)
                {
                    HandleDrawUtility.DrawRectBox(rect, Color.white);
                    EditorGUI.LabelField(rect, command.GetType().Name);

                    var removeButtonRect = new Rect(elementRect)
                    {
                        x    = elementRect.xMax - 16,
                        size = EditorGUIIcons.IconDefaultSize,
                    };
                    if (GUI.Button(removeButtonRect, EditorGUIIcons.RemoveButtonTex, GUI.skin.label))
                    {
                        data.Commands.RemoveAt(index);
                        List.ClearSelection();
                        EditorUtility.SetDirty(data);
                    }
                }
                else
                {
                    //icon表示
                    if(CommandValidator.Validate(command, out string errorLog))
                    {
                        drawer.DrawIcon(elementRect.position);
                    }
                    else
                    {
                        EditorGUI.LabelField(elementRect, EditorGUIIcons.ErrorIconTex);
                    }

                    //簡易表示
                    var guiRect = new Rect(elementRect)
                    {
                        xMin = elementRect.xMin + 32f,
                        xMax = elementRect.xMax - 16f,
                    };
                    drawer.DrawGUI(guiRect, command);

                    //複製ボタン
                    var duplicateButtonRect = new Rect(elementRect)
                    {
                        x    = elementRect.xMax - 64,
                        size = EditorGUIIcons.IconDefaultSize,
                    };
                    if (DrawDuplicateButton(duplicateButtonRect))
                    {
                        data.Commands.Insert(index, command.Clone());
                        EditorUtility.SetDirty(data);
                    }

                    //削除ボタン
                    var removeButtonRect = new Rect(elementRect)
                    {
                        x    = elementRect.xMax - 32,
                        size = EditorGUIIcons.IconDefaultSize,
                    };
                    if (DrawRemoveButton(removeButtonRect))
                    {
                        data.Commands.RemoveAt(index);
                        List.ClearSelection();
                        EditorUtility.SetDirty(data);
                    }

                    //PreviewAreaのFoldout
                    var foldoutRect = new Rect(elementRect)
                    {
                        x    = elementRect.xMax - 96,
                        size = EditorGUIIcons.IconDefaultSize,
                    };
                    if(drawer.DrawPreviewAreaFoldout(foldoutRect, command))
                    {
                        //PreviewAreaの表示
                        var property = so.FindProperty("m_commands").GetArrayElementAtIndex(index);
                        var subAreaRect = new Rect(guiRect)
                        {
                            y      = guiRect.yMax,
                            height = rect.height - guiRect.height,
                        };
                        drawer.DrawPreviewAreaGUI(subAreaRect, command, property);
                    }
                }
            }

            void DrawElementBackground(Rect rect, int index, bool isActive, bool isFocused)
            {            
                if(index < 0 || data.Commands.Count <= index) { return; }

                var command = data.Commands[index];
                var drawer  = command.FindDrawer();

                if(drawer != null)
                {
                    var color = drawer.FindDrawerGroupColor();
                    color.a = isFocused ? 0.3f : 0.05f;
                    EditorGUI.DrawRect(rect, color);
                }

                if (!CommandValidator.Validate(command, out string errorLog))
                {
                    var flagRect = new Rect(rect)
                    {
                        width = 20f
                    };
                    EditorGUI.DrawRect(flagRect, Color.red);
                }

                if (isFocused)
                {
                    HandleDrawUtility.DrawRectBox(rect, Color.white);
                }
            }

            float ElementHeight(int index)
            {
                var commandsProperty = so.FindProperty("m_commands");
                if (index < 0 || commandsProperty.arraySize <= index) { return 0f; }

                var command = data.Commands[index];
                var drawer  = command.FindDrawer();

                var property = commandsProperty.GetArrayElementAtIndex(index);

                return command.Foldout
                    ? drawer.GetPreviewAreaHeight(command, property)
                    : EditorGUIProperty.ElementHeight;
            }

            #endregion

            if(List == null)
            {
                List = new ReorderableList(data.Commands, typeof(BaseCommand))
                {
                    displayAdd    = false,
                    displayRemove = false,
                    multiSelect   = true,

                    drawHeaderCallback            = DrawHeader,
                    drawElementCallback           = DrawElement,
                    drawElementBackgroundCallback = DrawElementBackground,
                    elementHeightCallback         = ElementHeight,
                };
            }
            else
            {
                List.list = data.Commands;
            }

            //上から順に表示
            using (new GUILayout.HorizontalScope(GUILayout.Width(ResizeWidth.Value + 8)))
            using (var scroll = new GUILayout.ScrollViewScope(m_scrollPos, GUILayout.Width(ResizeWidth.Value)))
            {
                List.DoLayoutList();
                m_scrollPos = scroll.scrollPosition;
            }

            //枠を表示
            var reorderableListRect = GUILayoutUtility.GetLastRect();
            HandleDrawUtility.DrawRectBox(reorderableListRect, Color.white);

            float totalHeight = 0f;
            var labelRects = Enumerable.Range(0, List.count)
                .Select(index =>
                {
                    var rect = new Rect(reorderableListRect)
                    {
                        yMin   = -m_scrollPos.y + reorderableListRect.y + List.headerHeight + 1 + totalHeight,
                        height = ElementHeight(index) + 2,
                    };

                    totalHeight += rect.height;

                    return rect;
                }).ToArray();

            //drag and dropしたときの処理
            var ev       = Event.current;
            var mousePos = ev.mousePosition;

            var newCommand = CommandDrawer.CreateInstanceFunc?.Invoke();
            if(newCommand != null)
            {
                //マウスの位置にどのコマンドが挿入されるかを表示する
                var drawer = newCommand.FindDrawer();
                drawer.DrawLabel(mousePos);

                //挿入位置に線を引く
                if (reorderableListRect.Contains(mousePos) && labelRects.Any())
                {
                    for (int i = 0; i < labelRects.Length; i++)
                    {
                        if (mousePos.y >= labelRects[i].yMax) { continue; }

                        var rect = labelRects[i];
                        rect.height = 1;
                        HandleDrawUtility.DrawRectBox(rect, drawer.FindDrawerGroupColor());

                        break;
                    }

                    if(mousePos.y >= labelRects.Last().yMax) 
                    {
                        var rect = labelRects.Last();
                        rect.y = rect.yMax + 5;
                        rect.height = 1;
                        HandleDrawUtility.DrawRectBox(rect, drawer.FindDrawerGroupColor());
                    }

                    EditorGUIUtility.AddCursorRect(reorderableListRect, MouseCursor.Pan);
                }
            }
            
            //挿入
            if (ev.type == EventType.MouseUp)
            {
                if(reorderableListRect.Contains(mousePos) && newCommand != null)
                {
                    int insertIndex = 0;
                    for(int i=0; i<labelRects.Length; i++)
                    {
                        if(mousePos.y < labelRects[i].yMax) { continue; }
                        insertIndex = i + 1;
                    }
                    data.Commands.Insert(insertIndex, newCommand);
                    EditorUtility.SetDirty(data);
                }
                CommandDrawer.CreateInstanceFunc = null;
            }

            //右クリックでSubMenu表示
            if (reorderableListRect.Contains(mousePos) && ev.RightMouseDown())
            {
                var menu = new SubMenu();
                var selectedCommands = List.selectedIndices
                            .Select(i => data.Commands[i])
                            .ToArray();
                if (selectedCommands.Any())
                {
                    menu.AddItems(
                        new SubMenuItem("Copy Commands", () =>
                        {
                            ScenarioBuilderEditUtility.ClipBoardCommands = selectedCommands;
                            Debug.Log($"<color=orange>Copy Commands</color>");
                        }),
                        new SubMenuItem("Remove Commands", () =>
                        {
                            var newList = data.Commands
                                .Where((x, i) => !List.selectedIndices.Contains(i))
                                .ToArray();
                            data.Commands.Clear();
                            data.Commands.AddRange(newList);

                            List.ClearSelection();
                            EditorUtility.SetDirty(data);
                        })
                    );                    
                }
                if (ScenarioBuilderEditUtility.ClipBoardCommands.Any())
                {
                    menu.AddItems(
                        new SubMenuItem("Paset Command As New", ScenarioBuilderEditUtility.PasetAsNew)
                    );
                }
                menu.Show();
            }

            //ステートが並んでいない箇所での左クリックで選択をクリア
            var emptyRect = new Rect(reorderableListRect);
            if (labelRects.Any())
            {
                emptyRect.yMin = labelRects.Max(x => x.yMax);
            };
            if (emptyRect.Contains(mousePos) && ev.LeftMouseDown())
            {
                List.ClearSelection();
            }

            ResizeWidth.Calculate(reorderableListRect);
        }

        GUIStyle GetButtonStyle()
        {
            if(m_buttonStyle == null)
            {
                m_buttonStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleCenter, 
                };
            }
            return m_buttonStyle;
        }

        bool DrawDuplicateButton(Rect rect)
        {
            return GUI.Button(rect, EditorGUIIcons.DuplicateButtonTex, GetButtonStyle());
        }

        bool DrawRemoveButton(Rect rect)
        {
            return GUI.Button(rect, EditorGUIIcons.RemoveButtonTex, GetButtonStyle());
        }

        public void Reset()
        {
            List?.ClearSelection();

            m_scrollPos = Vector2.zero;
            List = null;

            ResizeWidth = null;
        }

    }
}
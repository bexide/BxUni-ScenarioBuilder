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
    /// 画面上部のヘッダー部分
    /// </summary>
    internal class WindowHeaderArea
    {
        #region const

        const float k_buttonWidth = 100f;

        #endregion

        #region property

        SubMenu m_subMenu = new SubMenu();

        IOrderedEnumerable<KeyValuePair<string, List<SubMenuItem>>> m_menuTable;

        #endregion

        internal void Initialize()
        {
            var menuTable = new Dictionary<string, List<SubMenuItem>>();

            var methods = TypeCache.GetMethodsWithAttribute<Editor.ScenarioBuilderEditorMenuItemAttribute>();
            foreach(var method in methods)
            {
                try
                {
                    if(!method.IsStatic)
                    {
                        throw new System.InvalidOperationException(
                            "Cannot be applied to other than static methods.");
                    }

                    var attr = ScenarioBuilderInternal.AttributeUtility.
                        GetMethodAttribute<Editor.ScenarioBuilderEditorMenuItemAttribute>(method);

                    var split       = attr.MenuItemName.Split('/');
                    string menuName = split[0];
                    string cmdName  = string.Join('/', split.Skip(1));

                    var subMenuItem = new SubMenuItem(cmdName, () => method?.Invoke(null, null), attr.Priority);

                    if (menuTable.TryGetValue(menuName, out var subMenus))
                    {
                        subMenus.Add(subMenuItem);
                    }
                    else
                    {
                        menuTable.Add(menuName, new List<SubMenuItem>()
                        {
                            subMenuItem
                        });
                    }
                }
                catch(System.Exception ex)
                {
                    Debug.LogException(ex);
                }

                try
                {
                    if (!ScenarioBuilderInternal.AttributeUtility
                            .TryGetMethodAttribute<Editor.ScenarioBuilderEditorMenuItemIconAttribute>(method, out var iconAttr))
                        continue;

                    var content = EditorGUIIcons.GetIcon(iconAttr.MenuItemIconName);

                    var subMenuItemIcon = new SubMenuItem(content, () => method?.Invoke(null, null), iconAttr.Priority);

                    if (!menuTable.ContainsKey(method.Name))
                    {
                        menuTable.Add(method.Name, new List<SubMenuItem>(1)
                        {
                            subMenuItemIcon
                        });
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            try
            {
                foreach (var kvp in menuTable)
                {
                    var list = kvp.Value;
                    if(list.Count <= 1) continue;
                    list = list.OrderByDescending(x => x.Priority)
                        .ThenBy(x => x.MenuName)
                        .ToList();

                    var newList = new List<SubMenuItem>();
                    int prePriority = -999999;
                    for (int i = 0; i < list.Count; i++)
                    {
                        newList.Add(list[i]);
                        if (i != 0 && list[i].Priority > prePriority + 10)
                        {
                            newList.Add(new SubMenuItem(string.Empty, null, list[i].Priority - 1));
                        }
                    }

                    menuTable[kvp.Key] = newList;
                }
            }
            catch { }

            m_menuTable = menuTable.OrderByDescending(kvp => kvp.Value.Max(v => v.Priority));
        }

        internal void DrawLayout()
        {
            static bool DrawLayoutButton(string label)
            {
                return GUILayout.Button(label, EditorStyles.toolbarButton,
                    GUILayout.Width(k_buttonWidth)
                );
            }

            static bool DrawIconButton(GUIContent content, out float squareWidth)
            {
                squareWidth = EditorStyles.toolbarButton.fixedHeight;
                return GUILayout.Button(content, EditorStyles.toolbarButton,
                    GUILayout.Width(squareWidth));
            }

            using var _ = new EditorGUILayout.HorizontalScope(EditorStyles.toolbar);

            var rect = new Rect(0, EditorGUIUtility.singleLineHeight, 1, 1);

            foreach(var kvp in m_menuTable)
            {
                if (kvp.Value.Count == 1)
                {
                    if (DrawIconButton(kvp.Value[0].Content, out float width))
                    {
                        kvp.Value[0].Invoke(true);
                    }
                    rect.x += width;
                    continue;
                }
                if(DrawLayoutButton(kvp.Key))
                {
                    m_subMenu
                        .ClearMenuItems()
                        .AddItems(kvp.Value.ToArray())
                        .Show(rect);
                }
                rect.x += k_buttonWidth;
            }
        }

    }
}
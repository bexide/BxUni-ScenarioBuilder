using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
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

            var methods = TypeCache.GetMethodsWithAttribute<Editor.CommandToolKitEditorMenuItemAttribute>();
            foreach(var method in methods)
            {
                try
                {
                    if(!method.IsStatic)
                    {
                        throw new System.InvalidOperationException(
                            "Cannot be applied to other than static methods.");
                    }

                    var attr = CommandToolKitInternal.AttributeUtility.
                        GetMethodAttribute<Editor.CommandToolKitEditorMenuItemAttribute>(method);

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
            }

            try
            {
                foreach (var kvp in menuTable)
                {
                    var list = kvp.Value;
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

            using var _ = new EditorGUILayout.HorizontalScope(EditorStyles.toolbar);

            var rect = new Rect(0, EditorGUIUtility.singleLineHeight, 1, 1);

            foreach(var kvp in m_menuTable)
            {
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
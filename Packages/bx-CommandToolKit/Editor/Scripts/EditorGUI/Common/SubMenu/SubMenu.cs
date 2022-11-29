using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
{
    internal class SubMenu
    {

        GenericMenu m_menu = new GenericMenu();

        internal void Show(Rect rect)
        {
            m_menu.DropDown(rect);
        }

        internal void Show()
        {
            m_menu.ShowAsContext();
        }

        internal SubMenu ClearMenuItems()
        {
            m_menu = new GenericMenu();
            return this;
        }

        internal SubMenu AddItems(params SubMenuItem[] items)
        {
            foreach(var item in items)
            {
                if(item.IsSeparator())
                {
                    m_menu.AddSeparator(string.Empty);
                }
                else
                {
                    m_menu.AddItem(item.Content, false, item.Invoke);
                }
            }

            return this;
        }
    }
}
using System;
using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    [Serializable]
    internal class CommandDrawerGroup
    {
#pragma warning disable 0649
        [SerializeField] string m_groupName;
        [SerializeField, ColorUsage(showAlpha:false)]
        Color m_color = Color.white;
        [SerializeField] CommandDrawer[] m_commandDrawers;
#pragma warning restore 0649

        Foldout m_foldout = new Foldout();

        internal CommandDrawer[] CommandDrawers => m_commandDrawers;

        internal Color Color => m_color.SetAlpha(1f);

        internal void DrawLayout()
        {
            if (m_foldout.DrawLayout(m_groupName, Color))
            {
                foreach(var drawer in m_commandDrawers)
                {
                    drawer.DrawLayout();
                    drawer.OnDragEnter();
                }
            }
        }

    }
}
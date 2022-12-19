//BeXide 2022-11-29
//by MurakamiKazuki

using System;
using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [Serializable]
    internal class CommandDrawerGroup
    {
#pragma warning disable 0649
        [Tooltip("グループ名")]
        [SerializeField] string m_groupName;

        [Tooltip("Editウィンドウ上で表示するかどうか")]
        [SerializeField] bool m_enabled = true;
        
        [Tooltip("グループを表す色"), ColorUsage(showAlpha:false)]
        [SerializeField] Color m_color = Color.white;
        
        [Tooltip("グループに所属するコマンド一覧")]
        [SerializeField] CommandDrawer[] m_commandDrawers;
#pragma warning restore 0649

        Foldout m_foldout = new Foldout();

        internal bool Enabled => m_enabled;

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
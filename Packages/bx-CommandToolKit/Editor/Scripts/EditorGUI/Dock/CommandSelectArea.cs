using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
{
    /// <summary>
    /// 画面中央左側のコマンド一覧表示部分
    /// </summary>
    internal class CommandSelectArea
    {

        #region property

        Vector2 m_scrollPos = Vector2.zero;

        internal ReSizeCalculator ResizeWidth { get; private set; }

        #endregion

        internal void DrawLayout(ScenarioData data)
        {
            if(ResizeWidth == null)
            {
                ResizeWidth = new ReSizeCalculator(200f, ReSizeCalculator.PositionType.right);
            }
            ResizeWidth.SetupMinMax(150f, 300f);

            var allCommandGroup = CommandRegistConfig.GetAllCommandGroup();
            using(var scroll = new EditorGUILayout.ScrollViewScope(m_scrollPos, GUILayout.Width(ResizeWidth.Value)))
            {
                foreach(var group in allCommandGroup)
                {
                    group.DrawLayout();
                }
                m_scrollPos = scroll.scrollPosition;
            }

            var lastRect = GUILayoutUtility.GetLastRect();
            var ev = Event.current;
            if (!lastRect.Contains(ev.mousePosition))
            {
                CommandDrawer.TooltipMessage = string.Empty;
            }

            //幅の変更
            ResizeWidth.Calculate(lastRect);
        }


    }
}
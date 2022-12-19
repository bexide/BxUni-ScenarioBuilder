//BeXide 2022-11-29
//by MurakamiKazuki

using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ReSizeCalculator
    {
        internal enum PositionType
        {
            left,

            right,
        }

        internal float Value { get; private set; }
        float m_minValue;
        float m_maxValue;

        PositionType m_positionType;
        bool isResizable = false;

        ReSizeCalculator m_joined = null;

        internal ReSizeCalculator(float value, PositionType positionType, ReSizeCalculator joined = null)
        {
            m_joined       = joined;         
            m_positionType = positionType;
            Value          = value - DifferenceJoinedValue();
        }

        public void SetupMinMax(float minValue, float maxValue) 
        {
            m_minValue = minValue;
            m_maxValue = maxValue;
        }

        internal void Calculate(Rect rect)
        {
            var resizeRect = new Rect(rect);
            if(m_positionType == PositionType.right)
            {
                resizeRect.x = rect.xMax - 5;
            }
            resizeRect.width = 5;

            var ev = Event.current;

            if (resizeRect.Contains(ev.mousePosition))
            {
                if (ev.LeftMouseDown())
                {
                    isResizable = true;
                }
                EditorGUIUtility.AddCursorRect(resizeRect, MouseCursor.ResizeHorizontal);
            }

            if (isResizable)
            {
                EditorGUIUtility.AddCursorRect(resizeRect, MouseCursor.ResizeHorizontal);

                if (ev.LeftMousePressed())
                {
                    Value = Mathf.Clamp(ev.mousePosition.x - DifferenceJoinedValue(), m_minValue, m_maxValue);
                }
                else if (ev.LeftMouseUp())
                {
                    isResizable = false;
                }
            }
        }

        float DifferenceJoinedValue()
        {
            return m_joined?.Value ?? 0f;
        }
    }
}
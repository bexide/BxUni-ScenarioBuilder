//BeXide 2022-11-29
//by MurakamiKazuki

using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal static class EventExtensions
    {
        const int k_LeftMouse  = 0;
        const int k_RightMouse = 1;

        internal static bool LeftMouseDown(this Event ev)
        {
            return OnMouseEvent(ev, EventType.MouseDown, k_LeftMouse);
        }

        internal static bool LeftMousePressed(this Event ev)
        {
            return OnMouseEvent(ev, EventType.MouseDrag, k_LeftMouse);
        }

        internal static bool LeftMouseUp(this Event ev)
        {
            return OnMouseEvent(ev, EventType.MouseUp, k_LeftMouse);
        }

        internal static bool RightMouseDown(this Event ev)
        {
            return OnMouseEvent(ev, EventType.MouseDown, k_RightMouse);
        }

        internal static bool RightMousePressed(this Event ev)
        {
            return OnMouseEvent(ev, EventType.MouseDrag, k_RightMouse);
        }

        internal static bool RightMouseUp(this Event ev)
        {
            return OnMouseEvent(ev, EventType.MouseUp, k_RightMouse);
        }

        static bool OnMouseEvent(Event ev, EventType type, int buttonNo)
        {
            return ev.type == type
                && ev.button == buttonNo;
        }
    }
}
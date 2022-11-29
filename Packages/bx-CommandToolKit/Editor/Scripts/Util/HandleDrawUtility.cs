using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.EditorInternal;

namespace BX.CommandToolKit.Editor
{
    public static class HandleDrawUtility
    {

        public static void DrawRectBox(Rect rect, Color color)
        {
            using var _ = new HandlesColorScope(color);
            Handles.DrawLines(new Vector3[]
            {
                new Vector3(rect.xMin, rect.yMin),
                new Vector3(rect.xMin, rect.yMax),
                new Vector3(rect.xMax, rect.yMax),
                new Vector3(rect.xMax, rect.yMin),
            },
            new int[]
            {
                0, 1,
                1, 2,
                2, 3,
                3, 0,
            });
        }

    }
}
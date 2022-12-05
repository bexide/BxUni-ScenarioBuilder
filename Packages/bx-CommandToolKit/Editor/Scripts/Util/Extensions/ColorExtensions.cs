using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    internal static class ColorExtensions
    {

        internal static Color SetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

    }
}
//BeXide 2022-12-05
//by MurakamiKazuki

using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
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
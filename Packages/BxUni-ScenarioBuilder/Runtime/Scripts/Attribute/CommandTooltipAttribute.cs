// BeXide 2025-08-21
// by MurakamiKazuki
using UnityEngine;

namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// BaseCommandを継承したクラスに付けることでTooltipのテキストを設定することが出来ます
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public sealed class CommandTooltipAttribute : System.Attribute
    {

        public string Tooltip { get; }

        public CommandTooltipAttribute(string tooltip)
        {
            Tooltip = tooltip;
        }
    }
}

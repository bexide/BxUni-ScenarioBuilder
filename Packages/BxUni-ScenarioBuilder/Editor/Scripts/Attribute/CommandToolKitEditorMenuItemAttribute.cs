//BeXide 2022-11-29
//by MurakamiKazuki

namespace BxUni.ScenarioBuilder.Editor
{
    /// <summary>
    /// BxUni-ScenarioBuilderウィンドウのメニューアイテムを追加するためのアトリビュート
    /// <para>※ static なメソッドに実装してください</para>
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public sealed class ScenarioBuilderEditorMenuItemAttribute : System.Attribute
    {
        internal string MenuItemName { get; }

        internal int Priority { get; }

        /// <summary>
        /// BxUni-ScenarioBuilderウィンドウのメニューアイテムを追加するためのアトリビュート
        /// <para>※ static なメソッドに実装してください</para>
        /// </summary>
        /// <param name="menuItem">メニュー名</param>
        /// <param name="priority">表示優先度</param>
        public ScenarioBuilderEditorMenuItemAttribute(string menuItem, int priority = 0)
        {
            MenuItemName = menuItem;
            Priority     = priority;
        }
    }

    /// <summary>
    /// BxUni-ScenarioBuilderウィンドウのメニューアイテムをアイコンで追加するためのアトリビュート
    /// <para>※ static なメソッドに実装してください</para>
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public sealed class ScenarioBuilderEditorMenuItemIconAttribute : System.Attribute
    {
        internal string MenuItemIconName { get; }

        internal int Priority { get; }

        /// <summary>
        /// BxUni-ScenarioBuilderウィンドウのメニューアイテムをアイコンで追加するためのアトリビュート
        /// <para>※ static なメソッドに実装してください</para>
        /// </summary>
        /// <param name="menuItemIconName">メニューアイコン名</param>
        /// <param name="priority">表示優先度</param>
        public ScenarioBuilderEditorMenuItemIconAttribute(string menuItemIconName, int priority = 0)
        {
            MenuItemIconName = menuItemIconName;
            Priority     = priority;
        }
    }
}

namespace BX.CommandToolKit.Editor
{
    /// <summary>
    /// bx-CommandToolKitウィンドウのメニューアイテムを追加するためのアトリビュート
    /// <para>※ static なメソッドに実装してください</para>
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public sealed class CommandToolKitEditorMenuItemAttribute : System.Attribute
    {
        internal string MenuItemName { get; }

        internal int Priority { get; }

        /// <summary>
        /// bx-CommandToolKitウィンドウのメニューアイテムを追加するためのアトリビュート
        /// <para>※ static なメソッドに実装してください</para>
        /// </summary>
        /// <param name="menuItem">メニュー名</param>
        /// <param name="priority">表示優先度</param>
        public CommandToolKitEditorMenuItemAttribute(string menuItem, int priority = 0)
        {
            MenuItemName = menuItem;
            Priority     = priority;
        }
    }
}
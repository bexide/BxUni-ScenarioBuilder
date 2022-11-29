
namespace BX.CommandToolKit.Editor
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public sealed class CommandToolKitEditorMenuItemAttribute : System.Attribute
    {
        public string MenuItemName { get; }

        public int Priority { get; }

        public CommandToolKitEditorMenuItemAttribute(string menuItem, int priority = 0)
        {
            MenuItemName = menuItem;
            Priority     = priority;
        }
    }
}
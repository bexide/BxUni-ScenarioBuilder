
namespace BX.CommandToolKit.Editor
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = true)]
    public sealed class CustomCommandEditorAttribute : System.Attribute
    {
        internal System.Type CustomCommandType { get; }

        public CustomCommandEditorAttribute(System.Type type)
        {
            CustomCommandType = type;
        }
    }
}
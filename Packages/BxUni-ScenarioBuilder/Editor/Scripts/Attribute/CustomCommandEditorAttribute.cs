
namespace BxUni.ScenarioBuilder.Editor
{
    /// <summary>
    /// CustomCommandEditorクラスを継承したクラスに実装してください
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = true)]
    public sealed class CustomCommandEditorAttribute : System.Attribute
    {
        internal System.Type CustomCommandType { get; }

        /// <summary>
        /// CustomCommandEditorクラスを継承したクラスに実装してください
        /// </summary>
        /// <param name="type">表示を変更するクラスのType</param>
        public CustomCommandEditorAttribute(System.Type type)
        {
            CustomCommandType = type;
        }
    }
}
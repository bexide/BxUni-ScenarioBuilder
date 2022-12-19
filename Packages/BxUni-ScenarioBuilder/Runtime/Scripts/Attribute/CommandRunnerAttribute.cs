//BeXide 2022-11-29
//by MurakamiKazuki


namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// Methodに付けることでコマンドの実行対象のMethodになります。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public sealed class CommandRunnerAttribute : System.Attribute
    {
        /// <summary>
        /// 実行対象のコマンドのタイプ
        /// </summary>
        public System.Type RunCommandType { get; }

        /// <summary>
        /// 引数に実行対象のコマンドのタイプを指定します。
        /// </summary>
        /// <param name="type">実行対象のコマンドのタイプ</param>
        public CommandRunnerAttribute(System.Type type)
        {
            RunCommandType = type;
        }

    }
}
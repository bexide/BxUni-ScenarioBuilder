
namespace BxUni.ScenarioBuilder 
{
    /// <summary>
    /// JumpCommandが流れてきた時に処理を実行するコンポーネント
    /// </summary>
    public class JumpCommandRunner : BaseCommandRunner
    {
        /// <summary>
        /// ラベルへジャンプ
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [CommandRunner(typeof(JumpCommand))]
        public string DoJump(JumpCommand command)
        {
            return command.TargetLabel;
        }

    }
}
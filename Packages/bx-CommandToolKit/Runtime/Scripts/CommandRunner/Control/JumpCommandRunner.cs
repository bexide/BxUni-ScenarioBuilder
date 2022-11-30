
namespace BX.CommandToolKit 
{
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
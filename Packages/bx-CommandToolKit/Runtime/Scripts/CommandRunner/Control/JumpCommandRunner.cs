
namespace BX.CommandToolKit 
{
    public class JumpCommandRunner : BaseCommandRunner
    {
        /// <summary>
        /// ラベルへジャンプ
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        [CommandRunner(typeof(JumpCommand))]
        public string DoJump(JumpCommand statement)
        {
            return statement.TargetLabel;
        }

    }
}
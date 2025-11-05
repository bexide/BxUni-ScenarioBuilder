using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.InsertScenario
{
    public class SampleCommandRunner : BaseCommandRunner
    {
        [SerializeField] CommandEngineDirector m_commandEngineDirector;

        [CommandRunner(typeof(LogCommand))]
        public void Log(LogCommand command)
        {
            Debug.Log(command.text);
        }

        [CommandRunner(typeof(InsertScenarioCommand))]
        public async Task InsertScenario(InsertScenarioCommand command, CancellationToken ct = default)
        {
            await m_commandEngineDirector.InsertScenarioTask(command.insertScenario, ct);
        }
    }
}

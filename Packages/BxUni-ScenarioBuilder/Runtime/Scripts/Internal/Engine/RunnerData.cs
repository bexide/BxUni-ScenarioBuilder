//BeXide 2022-11-29
//by MurakamiKazuki

using System.Reflection;
using BxUni.ScenarioBuilder;

namespace BxUni.ScenarioBuilderInternal
{
    internal class RunnerData
    {
        internal BaseCommandRunner Runner { get; }
        
        MethodInfo RunMethod { get; }

        internal RunnerData(BaseCommandRunner runner, MethodInfo methodInfo)
        {
            Runner    = runner;
            RunMethod = methodInfo;
        }

        internal bool IsReturnTypeCompair(System.Type type)
        {
            return RunMethod?.ReturnType == type;
        }

        internal object Invoke(params object[] args)
        {
            return RunMethod.Invoke(Runner, args);
        }

        internal T Invoke<T>(params object[] args)
        {
            return (T)Invoke(args);
        }

        internal void Reset()
        {
            Runner.ResetRunner();
        }

    }
}

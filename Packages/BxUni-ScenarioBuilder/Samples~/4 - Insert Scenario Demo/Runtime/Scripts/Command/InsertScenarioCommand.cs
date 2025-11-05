using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.InsertScenario
{
    [System.Serializable]
    public class InsertScenarioCommand : BaseCommand
    {
        public ScenarioData insertScenario;

        protected override string GetDefaultGUIText()
        {
            return insertScenario != null
                ? $"割り込み: {insertScenario.name}"
                : base.GetDefaultGUIText();
        }
    }
}

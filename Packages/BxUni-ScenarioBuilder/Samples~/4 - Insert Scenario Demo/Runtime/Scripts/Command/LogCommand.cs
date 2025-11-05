using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.InsertScenario
{
    [System.Serializable]
    public class LogCommand : BaseCommand
    {
        public string text;

        protected override string GetDefaultGUIText()
        {
            return $"Log: {text}";
        }
    }
}

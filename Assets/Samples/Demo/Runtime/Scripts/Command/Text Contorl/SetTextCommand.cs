using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    [System.Serializable]
    public class SetTextCommand : BaseCommand
    {
        [Header("表示するテキスト"), TextArea(1, 3)]
        [SerializeField] string m_text;

        public string Text => m_text;
    }
}
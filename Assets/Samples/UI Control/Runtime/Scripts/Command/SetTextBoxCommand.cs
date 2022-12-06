using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.UIControl
{
    /// <summary>
    /// テキストボックスに話者名とセリフを流すコマンド
    /// </summary>
    [System.Serializable]
    public class SetTextBoxCommand : BaseCommand
    {
        [SerializeField] string m_name;

        [TextArea(minLines: 1,maxLines: 3)]
        [SerializeField] string m_text;

        /// <summary>
        /// 話者名
        /// </summary>
        public string Name => m_name;

        /// <summary>
        /// テキスト
        /// </summary>
        public string Text => m_text;

    }
}
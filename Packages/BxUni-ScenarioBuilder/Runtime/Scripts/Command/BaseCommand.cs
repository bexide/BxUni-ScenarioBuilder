//BeXide 2022-12-05
//by MurakamiKazuki

namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// 全ステートの継承元クラス
    /// </summary>
    [System.Serializable]
    public abstract partial class BaseCommand
    {
        /// <summary>
        /// Editorで表示する時のテキストをカスタマイズできます。
        /// 表示の仕方など凝った表示をしたい場合は専用のEditor拡張を作る必要があります。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDefaultGUIText()
        {
            return string.Empty;
        }
    }
}
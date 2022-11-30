using UnityEngine;

namespace BX.CommandToolKit
{
    /// <summary>
    /// targetLabelで指定した文字列と一致するLabelCommandまでジャンプするコマンド。
    /// </summary>
    public class JumpCommand : BaseCommand
        , IJumpCommand
    {
#pragma warning disable 0649
        [SerializeField, LabelCommand] string m_targetLabel;
#pragma warning restore 0649

        /// <summary>
        /// ジャンプ先のラベル
        /// </summary>
        public string TargetLabel => m_targetLabel;

    }
}
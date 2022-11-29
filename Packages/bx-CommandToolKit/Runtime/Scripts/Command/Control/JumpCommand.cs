using UnityEngine;

namespace BX.CommandToolKit
{
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
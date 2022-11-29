using UnityEngine;

namespace BX.CommandToolKit
{
    [System.Serializable]
    public class LabelCommand : BaseCommand
    {
#pragma warning disable 0649
        [SerializeField] string m_name;
#pragma warning restore 0649

        /// <summary>
        /// ラベル名
        /// </summary>
        public string Name => m_name;
    }
}
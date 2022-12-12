using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    [System.Serializable]
    public class SelectButtonCommand : BaseCommand
        , IJumpCommand
    {
        [Header("選択肢のラベル一覧"), LabelCommand]
        [SerializeField] string[] m_labels;

        public string[] Labels => m_labels;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BX.CommandToolKit
{
    [System.Serializable]
    public partial class ScenarioData : ScriptableObject
    {
#pragma warning disable 0649
        [SerializeReference] List<BaseCommand> m_commands = new List<BaseCommand>();
#pragma warning restore 0649

        /// <summary>
        /// 登録したコマンド一覧
        /// </summary>
        public List<BaseCommand> Commands => m_commands;
    }
}
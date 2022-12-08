using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class TextControlRunner : BaseCommandRunner
    {
        #region Property

        [SerializeField] Text m_text;

        #endregion

        #region Command Methods

        /// <summary>
        /// SetTextCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetTextCommand))]
        public void SetText(SetTextCommand cmd)
        {
            m_text.text = cmd.Text;
        }

        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BxUni.ScenarioBuilder.Sample.UIControl.UI
{
    public class TextBox : MonoBehaviour
    {
        #region Property

        [SerializeField] Text m_nameArea;
        [SerializeField] Text m_textArea;

        #endregion

        /// <summary>
        /// 話者名を設定
        /// </summary>
        /// <param name="name">話者名</param>
        public void SetName(string name)
        {
            m_nameArea.text = name;
        }

        /// <summary>
        /// テキストを設定
        /// </summary>
        /// <param name="text">テキスト</param>
        public void SetText(string text)
        {
            m_textArea.text = text;
        }

        /// <summary>
        /// 表示している文字列を空にする
        /// </summary>
        public void Clear()
        {
            SetName(string.Empty);
            SetText(string.Empty);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.UIControl
{
    /// <summary>
    /// 以下のコマンドが流れた時に処理を行うコンポーネント
    /// <para>* SetTextBoxCommand</para>
    /// </summary>
    public class UIControlRunner : BaseCommandRunner
    {
        #region Property

        [SerializeField] UI.TextBox m_textBox;

        [SerializeField] RectTransform m_cursor;

        #endregion

        #region Command Methods

        /// <summary>
        /// SetTextBoxCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetTextBoxCommand))]
        public async Task SetTextBox(SetTextBoxCommand cmd, CancellationToken ct)
        {
            //話者名とセリフを設定
            m_textBox.SetName(cmd.Name);
            m_textBox.SetText(cmd.Text);

            Debug.Log(
                $"SetTextBox Name=[{cmd.Name}] Text=[{cmd.Text}]");


            //入力待ち
            m_cursor.gameObject.SetActive(true);
            
            bool isInput = false;
            while(!isInput)
            {
                ct.ThrowIfCancellationRequested();
                isInput = Input.anyKeyDown;
                await Task.Yield();
            }

            m_cursor.gameObject.SetActive(false);
        }

        #endregion

        public override void ResetRunner()
        {
            //リセット時に空文字にする
            m_textBox.Clear();
        }

    }
}
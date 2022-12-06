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

        #endregion

        #region Command Methods

        /// <summary>
        /// SetTextBoxCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetTextBoxCommand))]
        public void SetTextBox(SetTextBoxCommand cmd)
        {
            m_textBox.SetName(cmd.Name);
            m_textBox.SetText(cmd.Text);

            Debug.Log(
                $"SetTextBox Name=[{cmd.Name}] Text=[{cmd.Text}]");
        }

        #endregion

        public override void ResetRunner()
        {
            //リセット時に空文字にする
            m_textBox.Clear();
        }

    }
}
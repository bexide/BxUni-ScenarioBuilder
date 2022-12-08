using UnityEngine;
using UnityEngine.UI;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class BackgroundControlRunner : BaseCommandRunner
    {
        #region Property

        [SerializeField] Image m_backgroundImage;

        #endregion

        #region Command Methods

        /// <summary>
        /// BackgroundChangeCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(BackgroundChangeCommand))]
        public void BackgroundChange(BackgroundChangeCommand cmd)
        {
            m_backgroundImage.sprite = cmd.BackgroundSprite;
        }

        #endregion

        public override void ResetRunner()
        {
            m_backgroundImage.sprite = null;
        }
    }
}
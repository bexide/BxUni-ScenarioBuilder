using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class WaitControlRunner : BaseCommandRunner
    {
        #region Property

        [SerializeField] Button m_button;
        [SerializeField] SelectArea m_selectArea;

        #endregion

        #region Command Methods

        /// <summary>
        /// WaitClickCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        /// <param name="ct">キャンセルトークン</param>
        /// <returns></returns>
        [CommandRunner(typeof(WaitClickCommand))]
        public async Task WaitClickTask(WaitClickCommand cmd, CancellationToken ct)
        {
            bool isClick = false;

            void Click()
            {
                isClick = true;
            }

            try
            {
                m_button.onClick.AddListener(Click);
                while (!isClick)
                {
                    ct.ThrowIfCancellationRequested();
                    await Task.Yield();
                }
            }
            finally
            {
                m_button.onClick.RemoveListener(Click);
            }
        }

        [CommandRunner(typeof(SelectButtonCommand))]
        public async Task<string> SelectButtonTask(SelectButtonCommand cmd, CancellationToken ct)
        {
            try
            {
                m_selectArea.SetActive(true);

                int selected = await m_selectArea.SelectTask(cmd.Labels, ct);

                return cmd.Labels[selected];
            }
            finally
            {
                m_selectArea.SetActive(false);
            }
        }

        #endregion
    }
}
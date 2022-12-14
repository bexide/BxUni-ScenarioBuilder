using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class WaitControlRunner : BaseCommandRunner
    {
        #region Property

        [SerializeField] Button m_button;
        #endregion

        #region Command Methods

        /// <summary>
        /// WaitClickCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        /// <param name="ct">キャンセルトークン</param>
        /// <returns></returns>
        [CommandRunner(typeof(WaitClickCommand))]
        public async UniTask WaitClickTask(WaitClickCommand cmd, CancellationToken ct)
        {
            await m_button.OnClickAsync(ct);
        }

        #endregion
    }
}
using UnityEngine;

namespace BxUni.ScenarioBuilder 
{
    /// <summary>
    /// コマンドの実行対象クラス
    /// </summary>
    public abstract class BaseCommandRunner : MonoBehaviour
    {
        /// <summary>
        /// 各ランナーのリセット処理
        /// </summary>
        public virtual void ResetRunner()
        {

        }
    }
}
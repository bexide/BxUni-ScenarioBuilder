using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl
{
    /// <summary>
    /// IDに紐づくGameObjectのサイズを設定するコマンド
    /// </summary>
    [System.Serializable]
    public class SetScaleCommand : BaseCommand
    {
        [SerializeField] string m_id;
        [SerializeField] Vector3 m_scale = Vector3.one;

        /// <summary>
        /// 操作対象のGameObjectのID
        /// </summary>
        public string ID => m_id;

        /// <summary>
        /// 移動先のサイズ情報
        /// </summary>
        public Vector3 Scale => m_scale;
    }
}
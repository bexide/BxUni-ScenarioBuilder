using UnityEngine;

namespace BX.CommandToolKit.Sample.GameObjectControl
{
    /// <summary>
    /// IDに紐づくGameObjectの位置を設定するコマンド
    /// </summary>
    [System.Serializable]
    public class SetPositionCommand : BaseCommand
    {
        [SerializeField] string m_id;
        [SerializeField] Vector3 m_position;

        /// <summary>
        /// 操作対象のGameObjectのID
        /// </summary>
        public string ID => m_id;

        /// <summary>
        /// 移動先の位置情報
        /// </summary>
        public Vector3 Position => m_position;

    }
}
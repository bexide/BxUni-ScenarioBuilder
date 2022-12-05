using UnityEngine;

namespace BX.CommandToolKit.Sample.GameObjectControl
{
    /// <summary>
    /// IDに紐づくGameObjectの回転を設定するコマンド
    /// </summary>
    [System.Serializable]
    public class SetRotationCommand : BaseCommand
    {
        [SerializeField] string m_id;
        [SerializeField] Vector3 m_rotation;

        /// <summary>
        /// 操作対象のGameObjectのID
        /// </summary>
        public string ID => m_id;

        /// <summary>
        /// 移動先の回転情報
        /// </summary>
        public Vector3 Rotation => m_rotation;

    }
}
using UnityEngine;

namespace BX.CommandToolKit.Sample.GameObjectControl
{
    /// <summary>
    /// プリミティブなGameObjectを生成するコマンド
    /// </summary>
    [System.Serializable]
    public class PrimitiveGameObjectSpawnCommand : BaseCommand
    {
        [SerializeField] string m_id;
        [SerializeField] PrimitiveType m_primitiveType;

        /// <summary>
        /// 任意のID。
        /// </summary>
        public string ID => m_id;

        /// <summary>
        /// 生成するGameObjectのPrimitiveType
        /// </summary>
        public PrimitiveType PrimitiveType => m_primitiveType;
    }
}
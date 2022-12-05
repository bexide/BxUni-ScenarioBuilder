using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.GameObjectControl
{
    /// <summary>
    /// 以下のコマンドが流れた時に処理を行うコンポーネント
    /// <para>* PrimitiveGameObjectCommand</para>
    /// <para>* SetPositionCommand</para>
    /// <para>* SetRotationCommand</para>
    /// </summary>
    public class GameObjectControlRunner : BaseCommandRunner
    {
        #region Property

        Dictionary<string, GameObject> InstanceTable
            { get; set; } = new Dictionary<string, GameObject>();

        #endregion

        #region Command Methods

        /// <summary>
        /// PrimitiveGameObjectCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(PrimitiveGameObjectSpawnCommand))]
        public void PrimitiveGameObjectSpawn(PrimitiveGameObjectSpawnCommand cmd)
        {
            //既に指定のIDでオブジェクトが生成されていたら失敗扱い
            if (InstanceTable.ContainsKey(cmd.ID)) 
            {
                Debug.LogWarning(
                    $"The object associated with this ID has already been created. [{cmd.ID}]");
                return;
            }

            //オブジェクト生成
            var go = GameObject.CreatePrimitive(cmd.PrimitiveType);
            go.name = cmd.ID;

            //Dictionaryに保存
            InstanceTable.Add(cmd.ID, go);
            
            Debug.Log(
                $"Spawn Game Object ID=[{cmd.ID}] Primitive=[{cmd.PrimitiveType}]");
        }

        /// <summary>
        /// SetPositionCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetPositionCommand))]
        public void SetPosition(SetPositionCommand cmd)
        {
            //IDに紐づくGameObjectが生成されていなければ失敗扱い
            if(!InstanceTable.TryGetValue(cmd.ID, out var go))
            {
                Debug.LogWarning(
                    $"No object associated with this ID has been created. [{cmd.ID}]");
                return;
            }

            go.transform.localPosition = cmd.Position;
            Debug.Log(
                $"SetPosition ID=[{cmd.ID}] Position=[{cmd.Position}]");
        }

        /// <summary>
        /// SetRotationCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetRotationCommand))]
        public void SetRotation(SetRotationCommand cmd)
        {
            //IDに紐づくGameObjectが生成されていなければ失敗扱い
            if (!InstanceTable.TryGetValue(cmd.ID, out var go))
            {
                Debug.LogWarning(
                    $"No object associated with this ID has been created. [{cmd.ID}]");
                return;
            }

            go.transform.localEulerAngles = cmd.Rotation;
            Debug.Log(
                $"SetRotation ID=[{cmd.ID}] Rotation=[{cmd.Rotation}]");
        }

        /// <summary>
        /// SetScaleCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetScaleCommand))]
        public void SetScale(SetScaleCommand cmd)
        {
            //IDに紐づくGameObjectが生成されていなければ失敗扱い
            if (!InstanceTable.TryGetValue(cmd.ID, out var go))
            {
                Debug.LogWarning(
                    $"No object associated with this ID has been created. [{cmd.ID}]");
                return;
            }

            go.transform.localScale = cmd.Scale;
            Debug.Log(
                $"SetScale ID=[{cmd.ID}] Scale=[{cmd.Scale}]");
        }

        #endregion

        public override void ResetRunner()
        {
            //リセット時に現在生成しているオブジェクトを削除
            foreach(var go in InstanceTable.Values)
            {
                Destroy(go);
            }
            InstanceTable.Clear();
        }

    }
}
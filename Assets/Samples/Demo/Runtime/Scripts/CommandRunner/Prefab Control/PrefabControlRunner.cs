using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class PrefabControlRunner : BaseCommandRunner
    {
        #region Property

        Dictionary<string, GameObject> InstanceTable
            { get; set; } = new Dictionary<string, GameObject>();

        #endregion

        #region Command Methods

        /// <summary>
        /// PrefabSpawnSetupCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(PrefabSpawnSetupCommand))]
        public void PrefabSpawn(PrefabSpawnSetupCommand cmd)
        {
            foreach (var setup in cmd.Setups)
            {
                if (InstanceTable.ContainsKey(setup.ID))
                {
                    continue;
                }

                var go = Instantiate(setup.SpawnPrefab);
                go.name = setup.ID;
                go.SetActive(setup.IsActive);

                InstanceTable.TryAdd(setup.ID, go);
            }
        }

        /// <summary>
        /// ActiveSwitchCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(ActiveSwitchCommand))]
        public void ActiveSwitch(ActiveSwitchCommand cmd)
        {
            if(!InstanceTable.TryGetValue(cmd.ID, out var go)) { return; }

            go.SetActive(cmd.Active);
        }

        /// <summary>
        /// SetAnimationCommandが流れてきた時に処理を行う
        /// </summary>
        /// <param name="cmd">コマンドのパラメータ</param>
        [CommandRunner(typeof(SetAnimationCommand))]
        public void SetAnimation(SetAnimationCommand cmd)
        {
            if(!InstanceTable.TryGetValue(cmd.ID, out var go)) { return; }

            if(!go.TryGetComponent<Animator>(out var animator)) { return; }

            animator.Play(cmd.AnimationName);
        }

        #endregion

        public override void ResetRunner()
        {
            foreach(var go in InstanceTable.Values)
            {
                Destroy(go);
            }
            InstanceTable.Clear();
        }

    }
}
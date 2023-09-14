//BeXide 2023-09-14
//by MurakamiKazuki
using UnityEditor;
using UnityEditor.Callbacks;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    public class ScenarioDataOpener
    {
        /// <summary>
        /// 該当のシナリオファイルをダブルクリックで開けるようにする
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset]
        static bool OnOpen(int instanceID, int _)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID);

            if(asset is not ScenarioData scenarioData)
            {
                return false;
            }

            ScenarioEditFlowWindow.CreateWindow(scenarioData);
            return true;
        }

    }
}

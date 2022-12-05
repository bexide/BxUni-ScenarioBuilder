using UnityEngine;

namespace BxUni.ScenarioBuilder
{
    /// <summary>
    /// stringのFieldに付けることでシナリオ編集時にシナリオ内にあるラベルを指定できるようになります。
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public sealed class LabelCommandAttribute : PropertyAttribute
    {

    }
}
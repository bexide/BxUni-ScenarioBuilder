//BeXide 2022-11-29
//by MurakamiKazuki

using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class HandlesColorScope : BaseColorScope
    {
        internal HandlesColorScope(Color setColor) : base(setColor) { }

        internal override Color settingColor 
        {
            get => Handles.color;
            set => Handles.color = value;
        }
    }
}
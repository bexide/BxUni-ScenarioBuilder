using UnityEngine;
using UnityEditor;

namespace BX.CommandToolKit.EditorInternal
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
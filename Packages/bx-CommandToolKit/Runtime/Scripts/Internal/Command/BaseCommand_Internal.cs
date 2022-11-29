using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BX.CommandToolKit
{
    public abstract partial class BaseCommand
    {
#if UNITY_EDITOR
        internal bool Foldout { get; set; } = false;

        internal BaseCommand Clone()
        {
            string json = JsonUtility.ToJson(this);
            return (BaseCommand)JsonUtility.FromJson(json, GetType());
        }
#endif
    }
}

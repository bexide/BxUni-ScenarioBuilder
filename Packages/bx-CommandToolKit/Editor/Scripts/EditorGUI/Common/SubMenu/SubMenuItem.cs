using System;
using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    internal class SubMenuItem
    {
        #region property

        internal string MenuName => Content.text;

        internal GUIContent Content { get; }

        internal int Priority { get; }

        Action Callback { get; }

        #endregion

        internal SubMenuItem(string name, Action callback, int priority = 0)
        {
            Callback = callback;
            Priority = priority;
            Content  = new GUIContent(name);
        }

        internal bool IsSeparator()
        {
            return string.IsNullOrEmpty(MenuName);
        }

        internal void Invoke()
        {
            if (IsSeparator()) { return; }

            Debug.Assert(Callback != null, "Callback not configured...");
            Callback?.Invoke();
        }

    }
}
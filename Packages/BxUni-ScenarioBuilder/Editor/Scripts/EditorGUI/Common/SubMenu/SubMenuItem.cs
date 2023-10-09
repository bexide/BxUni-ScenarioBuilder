//BeXide 2022-11-29
//by MurakamiKazuki

using System;
using UnityEngine;

namespace BxUni.ScenarioBuilder.EditorInternal
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

        internal SubMenuItem(GUIContent content, Action callback, int priority = 0)
        {
            Callback = callback;
            Priority = priority;
            Content  = content;
        }

        internal bool IsSeparator()
        {
            return string.IsNullOrEmpty(MenuName);
        }

        internal void Invoke(bool isIcon = false)
        {
            if (IsSeparator() && !isIcon) { return; }

            Debug.Assert(Callback != null, "Callback not configured...");
            Callback?.Invoke();
        }
    }
}
using UnityEngine;
using UnityEditor;
using BX.CommandToolKit.EditorInternal;

namespace BX.CommandToolKit.Editor
{
    public abstract class CustomCommandEditor
    {
        static readonly internal string SetupMethodName = nameof(Setup);
        static readonly internal string OnGUIMethodName = nameof(OnGUI);
        static readonly internal string HasPreviewAreaMethodName = nameof(HasPreviewArea);
        static readonly internal string OpPreviewAreaGUIMethodName = nameof(OnPreviewAreaGUI);
        static readonly internal string GetPreviewAreaHeightMethodName = nameof(GetPreviewAreaHeight);

        protected BaseCommand target { get; private set; }

        internal void Setup(BaseCommand command)
        {
            target = command;
        }

        public virtual void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"[{target.FindDrawer().ViewNameText}]");
        }

        public virtual bool HasPreviewArea()
        {
            return false;
        }

        public virtual void OnPreviewAreaGUI(Rect rect, SerializedProperty property)
        {

        }

        public virtual float GetPreviewAreaHeight()
        {
            return 128.0f;
        }
    }
}
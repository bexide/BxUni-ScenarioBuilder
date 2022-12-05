using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.EditorInternal;

namespace BxUni.ScenarioBuilder.Editor
{
    /// <summary>
    /// このクラスを継承することでBxUni-ScenarioBuilderウィンドウ編集中のコマンド描画方法をカスタマイズできます。
    /// <para>※ このクラスを継承し、CustomCommandEditorAttributeを継承する必要があります。</para>
    /// </summary>
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

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="rect"></param>
        public virtual void OnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, $"[{target.FindDrawer().ViewNameText}]");
        }

        /// <summary>
        /// Previewエリアを表示するか
        /// </summary>
        /// <returns></returns>
        public virtual bool HasPreviewArea()
        {
            return false;
        }

        /// <summary>
        /// Previewエリア内の描画処理。
        /// <para>HasPreviewAreaをoverrideし、TRUEを返す必要があります。</para>
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        public virtual void OnPreviewAreaGUI(Rect rect, SerializedProperty property)
        {

        }

        /// <summary>
        /// Previewエリア内の表示の高さ
        /// </summary>
        /// <returns></returns>
        public virtual float GetPreviewAreaHeight()
        {
            return 128.0f;
        }

        /// <summary>
        /// 有効なコマンドかチェックする
        /// </summary>
        /// <param name="errorLog">失敗時のエラーログ</param>
        /// <returns></returns>
        public virtual bool Validate(out string errorLog)
        {
            errorLog = string.Empty;
            return true;
        }
    }
}
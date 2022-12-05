using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal partial class ScenarioEditFlowWindow : EditorWindow
    {
        #region const

        const string k_editorName = "BxUni-ScenarioBuilder";

        #endregion

        #region property

        internal static ScenarioEditFlowWindow Instance { get; private set; } = null;

        /// <summary>
        /// 編集中のシナリオデータ
        /// </summary>
        internal ScenarioData CurrentEditData { get; private set; } = null;

        SerializedObject SerializedObject { get; set; } = null;

        #endregion

        #region Dock

        internal ScenarioFileInfoArea  FileInfoArea { get; } = new ScenarioFileInfoArea();
        internal ScenarioEditArea  FlowEditArea { get; } = new ScenarioEditArea();
        internal WindowHeaderArea    HeaderArea   { get; } = new WindowHeaderArea();
        internal CommandHelpArea      HelpArea     { get; } = new CommandHelpArea();
        internal CommandInspectorArea InspectorArea{ get; } = new CommandInspectorArea();
        internal CommandSelectArea    SelectArea   { get; } = new CommandSelectArea();

        #endregion

        [MenuItem("BeXide/ScenarioBuilder/Edit")]
        static void CreateWindow()
        {
            var window = GetWindow<ScenarioEditFlowWindow>(title: k_editorName);
            window.Initialize();
        }

        internal static void CreateWindow(ScenarioData editData)
        {
            var window = GetWindow<ScenarioEditFlowWindow>(title: k_editorName);
            window.Initialize();
            window.CurrentEditData = editData;
        }

        void Initialize()
        {
            Instance = this;
            CommandRegistConfig.ResetCache();

            SerializedObject = null;

            HeaderArea.Initialize();
            FlowEditArea.Reset();
        }

        void OnGUI()
        {
            //ヘッダー描画
            HeaderArea.DrawLayout();

            if(CurrentEditData == null)
            {
                using var _  = new VerticalCenterScope();
                using var __ = new HorizontalCenterScope();

                if (GUILayout.Button("新規作成", GUILayout.Width(200)))
                {
                    ScenarioBuilderEditUtility.CreateAsset();
                }
                if (GUILayout.Button("開く", GUILayout.Width(200)))
                {
                    ScenarioBuilderEditUtility.OpenAsset();
                }
            }
            else
            {
                //画面上
                FileInfoArea.DrawLayout(CurrentEditData);

                using var change = new EditorGUI.ChangeCheckScope();
                using var _      = new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true));

                if (SerializedObject == null)
                {
                    SerializedObject = new SerializedObject(CurrentEditData);
                }
                SerializedObject.Update();

                //画面左
                SelectArea.DrawLayout(CurrentEditData);

                //画面中央
                FlowEditArea.DrawLayout(CurrentEditData, SerializedObject);

                //画面右
                InspectorArea.DrawLayout(
                    CurrentEditData, SerializedObject, FlowEditArea.SelectedIndices);

                if (change.changed)
                {
                    SerializedObject.ApplyModifiedProperties();
                }
            }

            //画面下
            HelpArea.DrawLayout();

            Repaint();
        }

        void OnEnable()
        {
            Initialize();
        }

        void OnDestroy()
        {
            Instance = null;
        }

    }
}
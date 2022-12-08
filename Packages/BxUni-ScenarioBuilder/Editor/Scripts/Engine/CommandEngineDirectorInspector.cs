using System.Linq;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilderInternal;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [CustomEditor(typeof(CommandEngineDirector))]
    internal class CommandEngineDirectorInspector : UnityEditor.Editor
    {

        CommandEngineDirector Target => target as CommandEngineDirector;

        static Foldout s_foldout = new Foldout();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var scenarioAssetProp = serializedObject.FindProperty("m_scenarioAsset");
            var playOnAwakeProp   = serializedObject.FindProperty("m_playOnAwake");

            //scenarioAsset
            EditorGUILayout.PropertyField(scenarioAssetProp);
            using (new EditorGUI.DisabledGroupScope(scenarioAssetProp.objectReferenceValue == null))
            {
                if (GUILayout.Button("Open"))
                {
                    ScenarioEditFlowWindow.CreateWindow(Target.scenarioAsset);
                }
            }

            //playOnAwake
            EditorGUILayout.PropertyField(playOnAwakeProp);

            using (new EditorGUI.DisabledGroupScope(!EditorApplication.isPlaying))
            {
                if (GUILayout.Button("Reset"))
                {
                    Target.ResetFlow();
                }
            }

            DrawLayoutChildRunners();

            serializedObject.ApplyModifiedProperties();
        }

        void DrawLayoutChildRunners()
        {
            if(!s_foldout.DrawLayout("CommandRunners", Color.green)) { return; }

            var runners = Target.GetComponentsInChildren<BaseCommandRunner>()
                .SelectMany(runner =>
                {
                    return runner.GetType().GetMethods()
                        .Where(AttributeUtility.HasMethodAttribute<CommandRunnerAttribute>)
                        .Select(method =>
                        {
                            var attr = AttributeUtility.GetMethodAttribute<CommandRunnerAttribute>(method);
                            return (type: attr.RunCommandType, runner: runner);
                        });
                });

            var width = GUILayout.Width(EditorGUIUtility.currentViewWidth / 3f);

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("[Runner Object]", width);
                EditorGUILayout.LabelField("[Runner Component]", width);
                EditorGUILayout.LabelField("[Command Type]", width);
            }

            var rect = GUILayoutUtility.GetLastRect();
            rect.y = rect.yMax;
            rect.height = 1f;
            HandleDrawUtility.DrawRectBox(rect, Color.green);

            foreach (var (type, runner) in runners)
            {
                using var _ = new EditorGUILayout.HorizontalScope();

                if (GUILayout.Button(runner.name, EditorStyles.linkLabel, width))
                {
                    Selection.activeObject = runner;
                }
                EditorGUILayout.LabelField(runner.GetType().Name, width);
                EditorGUILayout.LabelField(type.Name, width);
            }
        }
    }
}
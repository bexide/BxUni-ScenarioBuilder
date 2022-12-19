//BeXide 2022-12-08
//by MurakamiKazuki

using System.Linq;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilderInternal;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [CustomEditor(typeof(BaseCommandRunner), editorForChildClasses: true)]
    internal class CommandRunnerInspector : UnityEditor.Editor
    {
        BaseCommandRunner Target => target as BaseCommandRunner;

        Foldout m_foldout = new Foldout();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(1);
            var rect = GUILayoutUtility.GetLastRect();
            HandleDrawUtility.DrawRectBox(rect, Color.green);

            if (!m_foldout.DrawLayout("CommandRunners", Color.green)) { return; }

            var types = Target.GetType().GetMethods()
                .Where(AttributeUtility.HasMethodAttribute<CommandRunnerAttribute>)
                .Select(method =>
                {
                    var attr = AttributeUtility.GetMethodAttribute<CommandRunnerAttribute>(method);
                    return attr.RunCommandType;
                });

            foreach(var type in types)
            {
                EditorGUILayout.LabelField($"* {type.FullName}", EditorStyles.boldLabel);
            }
        }
    }
}
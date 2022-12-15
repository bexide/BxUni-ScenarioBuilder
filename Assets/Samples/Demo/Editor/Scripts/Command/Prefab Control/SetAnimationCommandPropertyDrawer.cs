using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace BxUni.ScenarioBuilder.Sample.Demo.Editor
{
    [CustomPropertyDrawer(typeof(SetAnimationCommand))]
    public class SetAnimationCommandPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float idPropertyHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("m_id"));
            return idPropertyHeight + EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var so = property.serializedObject;
            if (!(so.targetObject is ScenarioData scenarioData))
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var idProperty = property.FindPropertyRelative("m_id");
            var animationNameProperty = property.FindPropertyRelative("m_animationName");

            var rect = new Rect(position)
            {
                x = 20,
                height = EditorGUI.GetPropertyHeight(idProperty),
            };

            //id
            EditorGUI.PropertyField(rect, idProperty);
            rect.y += rect.height;
            rect.height = EditorGUIUtility.singleLineHeight;

            //animationName
            var target = scenarioData.Commands
                        .Where(cmd => cmd is PrefabSpawnSetupCommand)
                        .SelectMany(cmd => (cmd as PrefabSpawnSetupCommand).Setups)
                        .Where(setup => setup.ID == idProperty.stringValue)
                        .FirstOrDefault(setup => setup.SpawnPrefab);
            if(target?.SpawnPrefab == null)
            {
                EditorGUI.PropertyField(rect, animationNameProperty);
                return;
            }

            if(!target.SpawnPrefab.TryGetComponent<Animator>(out var animator))
            {
                EditorGUI.PropertyField(rect, animationNameProperty);
                return;
            }

            var names = animator.runtimeAnimatorController.animationClips
                .Select(clip => clip.name)
                .ToArray();

            if (!names.Any())
            {
                EditorGUI.LabelField(rect, $"Animationが存在しません");
                return;
            }

            int currentIndex = 0;
            try
            {
                currentIndex = System.Array.IndexOf(names, animationNameProperty.stringValue);
            }
            catch { }

            currentIndex = Mathf.Clamp(currentIndex, 0, names.Length - 1);
            currentIndex = EditorGUI.Popup(rect, animationNameProperty.name, currentIndex, names);
            animationNameProperty.stringValue = names[currentIndex];
        }

    }
}
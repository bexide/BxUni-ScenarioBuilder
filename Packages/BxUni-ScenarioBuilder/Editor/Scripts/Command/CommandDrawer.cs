//BeXide 2022-11-29
//by MurakamiKazuki

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;
using BxUni.ScenarioBuilderInternal;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [Serializable]
    internal class CommandDrawer
    {
#pragma warning disable 0649
        [SerializeField] string m_viewName;
        [SerializeField] Texture2D m_icon;
        [MonoScriptSelector]
        [SerializeField] MonoScript m_script;
        [SerializeField] string m_tooltip;
#pragma warning restore 0649

        internal string ViewNameText => m_viewName;

        internal static string TooltipMessage { get; set; } = string.Empty;

        internal static Func<BaseCommand> CreateInstanceFunc { get; set; }

        GUIContent Icon => m_icon ? new GUIContent(m_icon) : EditorGUIIcons.DefaultCommandIcon;

        GUIContent ViewName => new GUIContent(m_viewName);

        List<Type> m_customCommandEditorTypes = new List<Type>();

        /// <summary>
        /// 型情報の取得
        /// </summary>
        /// <returns></returns>
        internal Type GetClassType()
        {
            Type type = null;
            try
            {
                type = m_script?.GetClass();
            }
            catch { }
            return type;
        }

        internal void OnDragEnter()
        {
            var rect = GUILayoutUtility.GetLastRect();
            var ev   = Event.current;

            var bgColor = this.FindDrawerGroupColor();
            HandleDrawUtility.DrawRectBox(rect, bgColor);

            if (rect.Contains(ev.mousePosition))
            {
                var classType = GetClassType();

                string tooltip = m_tooltip;
                if(string.IsNullOrEmpty(tooltip) 
                && AttributeUtility.TryGetClassAttribute<CommandTooltipAttribute>(classType, out var tooltipAttribute))
                {
                    tooltip = tooltipAttribute.Tooltip;
                }

                TooltipMessage = tooltip;

                EditorGUIUtility.AddCursorRect(rect, MouseCursor.Pan);

                bgColor.a = 0.1f;
                EditorGUI.DrawRect(rect, bgColor);

                if(!ev.LeftMouseDown()) { return; }

                if(typeof(BaseCommand).IsAssignableFrom(classType))
                {
                    CreateInstanceFunc = 
                        () => CreateBaseCommandInstanceFuncImpl(classType);
                }
                else
                {
                    CreateInstanceFunc = null;
                    Debug.LogWarning("Type mismatch...");
                }
            }
        }

        BaseCommand CreateBaseCommandInstanceFuncImpl(Type classType)
        {
            return Activator.CreateInstance(classType) as BaseCommand;
        }

        object CreateCustomCommandEditorInstance(Type t, BaseCommand command)
        {
            var instance  = Activator.CreateInstance(t);
            var bindFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetMethod(CustomCommandEditor.SetupMethodName, bindFlags)
             .Invoke(instance, new object[] { command });
            return instance;
        }

        void Rebuild()
        {
            if(m_customCommandEditorTypes.Count > 0) { return; }

            var type  = GetClassType();
            var types = TypeCache.GetTypesWithAttribute<CustomCommandEditorAttribute>();
            foreach(var t in types)
            {
                if (!t.IsSubclassOf(typeof(CustomCommandEditor))) { continue; }

                var attributes = t.GetCustomAttributes(typeof(CustomCommandEditorAttribute), false);
                foreach(CustomCommandEditorAttribute attr in attributes)
                {
                    if(attr.CustomCommandType  != type) { continue; }
                    m_customCommandEditorTypes.Add(t);
                }
            }
        }

        #region DrawGUI

        internal void DrawIcon(Vector2 pos)
        {
            var rect = new Rect(pos, EditorGUIIcons.IconDefaultSize);
            using var _ = new ContentColorScope(this.FindDrawerGroupColor());
            EditorGUI.LabelField(rect, Icon);
        }

        internal void DrawLabel(Vector2 pos)
        {
            var iconSize = EditorGUIIcons.IconDefaultSize;
            iconSize.x *= 10;

            pos.x -= iconSize.x * 0.5f;
            pos.y -= iconSize.y * 0.5f;
            var rect = new Rect(pos, iconSize);

            var skin = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter
            };

            var color = this.FindDrawerGroupColor();

            using var _ = new BackgroundColorScope(color);
            EditorGUI.LabelField(rect, ViewName , skin);
            
            HandleDrawUtility.DrawRectBox(rect, color);
        }

        internal void DrawGUI(Rect rect, BaseCommand command)
        {
            Rebuild();

            //Editor拡張されている場合
            if (m_customCommandEditorTypes.Any())
            {
                foreach (var t in m_customCommandEditorTypes)
                {
                    try
                    {
                        var instance = CreateCustomCommandEditorInstance(t, command);
                        t.GetMethod(CustomCommandEditor.OnGUIMethodName)
                         .Invoke(instance, new object[] { rect });
                    }
                    catch(Exception ex)
                    {
                        Debug.LogException(ex);
                        EditorGUI.LabelField(rect, $"[{command.FindDrawer().m_viewName}]");
                    }
                }
            }
            //されていない場合
            else
            {
                EditorGUI.LabelField(rect, $"[{command.FindDrawer().m_viewName}]");
            }
        }

        internal bool DrawPreviewAreaFoldout(Rect rect, BaseCommand command)
        {
            Rebuild();

            bool hasPreviewArea = false;

            if (m_customCommandEditorTypes.Any())
            {
                foreach (var t in m_customCommandEditorTypes)
                {
                    try
                    {
                        var instance = CreateCustomCommandEditorInstance(t, command);
                        hasPreviewArea |= (bool)t.GetMethod(CustomCommandEditor.HasPreviewAreaMethodName)
                         .Invoke(instance, new object[] { });
                    }
                    catch(Exception ex)
                    {
                        Debug.LogException(ex);
                        hasPreviewArea = false;
                    }
                }
            }

            //Previewは表示しない
            if (!hasPreviewArea) { return false; }

            command.Foldout = EditorGUI.Foldout(rect, command.Foldout, GUIContent.none);

            return command.Foldout;
        }
        
        internal void DrawPreviewAreaGUI(Rect rect, BaseCommand command, SerializedProperty property)
        {
            Rebuild();

            if (m_customCommandEditorTypes.Any())
            {
                foreach (var t in m_customCommandEditorTypes)
                {
                    try
                    {
                        var instance = CreateCustomCommandEditorInstance(t, command);
                        t.GetMethod(CustomCommandEditor.OpPreviewAreaGUIMethodName)
                         .Invoke(instance, new object[] { rect, property });
                    }
                    catch(Exception ex)
                    {
                        Debug.LogException(ex);
                        HandleDrawUtility.DrawRectBox(rect, Color.red);
                        EditorGUI.LabelField(rect, $"Previewが実行できません。Consoleからエラー内容をご確認ください");
                    }
                }
            }
        }

        internal float GetPreviewAreaHeight(BaseCommand command, SerializedProperty property)
        {
            Rebuild();


            if (m_customCommandEditorTypes.Any())
            {
                float height = -1f;
                foreach (var t in m_customCommandEditorTypes)
                {
                    try
                    {
                        var instance = CreateCustomCommandEditorInstance(t, command);
                        float h = (float)t.GetMethod(CustomCommandEditor.GetPreviewAreaHeightMethodName)
                         .Invoke(instance, new object[] { property });

                        height = Math.Max(height, h);
                    }
                    catch(Exception ex)
                    {
                        Debug.LogException(ex);
                        height = -1;
                    }
                }
                return height > 0.0f ? height : 128.0f; 
            }
            else
            {
                return 128.0f;
            }
        }

        #endregion

        #region DrawGUILayout

        internal void DrawLayout(params GUILayoutOption[] options)
        {
            using var _ = new GUILayout.HorizontalScope(options);

            var size = EditorGUIIcons.IconDefaultSize;
            using (new ContentColorScope(this.FindDrawerGroupColor()))
            {
                EditorGUILayout.LabelField(Icon, EditorGUIIcons.IconSizeLayoutOption(size));
            }
            EditorGUILayout.LabelField(ViewName);
        }

        #endregion
    }
}
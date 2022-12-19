//BeXide 2022-11-29
//by MurakamiKazuki

using System;
using System.Linq;
using UnityEditor;
using BxUni.ScenarioBuilder.Editor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal static class CommandValidator
    {
        static CustomCommandEditor[] s_customEditors = new CustomCommandEditor[0];

        /// <summary>
        /// 有効なステートかチェックする
        /// </summary>
        /// <typeparam name="T">チェックするステートの型</typeparam>
        /// <param name="errorLog">失敗時のエラーログ</param>
        /// <returns></returns>
        internal static bool Validate(BaseCommand command, out string errorLog)
        {
            errorLog = string.Empty;

            if(command == null) { return true; }

            var editor = FindCustomCommandEditor(command);
            if(editor == null)
            {//Validatorは特に見つからなかったのでtrue
                return true;
            }

            return editor.Validate(out errorLog);
        }

        static CustomCommandEditor FindCustomCommandEditor(BaseCommand command)
        {
            Rebuild();

            var targetType = command.GetType();

            var editor = s_customEditors.FirstOrDefault(x => 
            {
                var attr = x.GetType()
                            .GetCustomAttributes(typeof(CustomCommandEditorAttribute), inherit: true)
                            .FirstOrDefault();
                if(attr is CustomCommandEditorAttribute custom)
                {
                    return custom.CustomCommandType == targetType;
                }
                else
                {
                    return false;
                }
            });

            if(editor != null)
            {
                editor.Setup(command);
            }

            return editor;
        }

        static void Rebuild()
        {
            if (s_customEditors.Any()) { return; }

            var types = TypeCache.GetTypesWithAttribute<CustomCommandEditorAttribute>();
            s_customEditors = types
                .Select(type => (CustomCommandEditor)Activator.CreateInstance(type))
                .ToArray();
        }
    }
}
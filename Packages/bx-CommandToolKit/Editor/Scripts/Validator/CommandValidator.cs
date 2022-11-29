using System;
using System.Linq;
using UnityEditor;
using BX.CommandToolKit.Editor;

namespace BX.CommandToolKit.EditorInternal
{
    internal static class CommandValidator
    {
        static ICommandValidator[] s_usingValidators = new ICommandValidator[0];

        /// <summary>
        /// 有効なステートかチェックする
        /// </summary>
        /// <typeparam name="T">チェックするステートの型</typeparam>
        /// <param name="errorLog">失敗時のエラーログ</param>
        /// <returns></returns>
        internal static bool Validate(BaseCommand command, out string errorLog)
        {
            errorLog = string.Empty;

            var validator = FindValidator(command);
            if(validator == null)
            {//Validatorは特に見つからなかったのでtrue
                return true;
            }

            return validator.Validate(command, out errorLog);
        }

        static ICommandValidator FindValidator(BaseCommand command)
        {
            Rebuild();

            var targetType = command.GetType();

            var target = s_usingValidators.FirstOrDefault(x => x.TargetType == targetType);
            if(target is CustomCommandEditor editor)
            {
                editor.Setup(command);
            }

            return target;
        }

        static void Rebuild()
        {
            if (s_usingValidators.Any()) { return; }

            var types = TypeCache.GetTypesDerivedFrom<ICommandValidator>();
            s_usingValidators = types
                .Select(type => (ICommandValidator)Activator.CreateInstance(type))
                .ToArray();
        }
    }
}
using System;

namespace BX.CommandToolKit.Editor
{
    /// <summary>
    /// コマンド内のデータが正しく実装されているか
    /// </summary>
    public interface ICommandValidator
    {
        Type TargetType { get; }

        bool Validate(BaseCommand command, out string errorLog);
    }
}
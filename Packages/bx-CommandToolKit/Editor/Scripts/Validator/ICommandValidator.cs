using System;

namespace BX.CommandToolKit.Editor
{
    public interface ICommandValidator
    {
        Type TargetType { get; }

        bool Validate(BaseCommand command, out string errorLog);
    }
}
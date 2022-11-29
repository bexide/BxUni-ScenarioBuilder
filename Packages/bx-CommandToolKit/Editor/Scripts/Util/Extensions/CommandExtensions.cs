using System.Linq;

namespace BX.CommandToolKit.EditorInternal
{
    internal static class CommandExtensions
    {

        internal static CommandDrawer FindDrawer<T>(this T command)
            where T : BaseCommand
        {
            var groups = CommandRegistConfig.GetAllCommandGroup();
            if(groups == null) { return null; }

            return groups
                .SelectMany(group => group.CommandDrawers)
                .FirstOrDefault(x => x.GetClassType() == command.GetType());
        }

    }
}
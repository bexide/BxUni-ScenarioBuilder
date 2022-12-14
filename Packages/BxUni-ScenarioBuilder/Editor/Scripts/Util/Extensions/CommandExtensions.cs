using System.Linq;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal static class CommandExtensions
    {

        internal static CommandDrawer FindDrawer<T>(this T command)
            where T : BaseCommand
        {
            var groups = CommandRegistConfig.GetAllCommandGroup();
            if(groups == null) { return null; }

            var drawer = groups
                .SelectMany(group => group.CommandDrawers)
                .FirstOrDefault(x => x.GetClassType() == command?.GetType());

            return drawer;
        }

    }
}
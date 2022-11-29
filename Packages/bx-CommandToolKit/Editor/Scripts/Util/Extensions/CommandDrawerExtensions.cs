using System.Linq;
using UnityEngine;

namespace BX.CommandToolKit.EditorInternal
{
    internal static class CommandDrawerExtensions
    {

        internal static CommandDrawerGroup FindDrawerGroup(this CommandDrawer drawer)
        {
            var groups = CommandRegistConfig.GetAllCommandGroup();
            if(groups == null) { return null; }

            return groups.FirstOrDefault(x => x.CommandDrawers.Contains(drawer));
        }

        internal static Color FindDrawerGroupColor(this CommandDrawer drawer)
        {
            return drawer.FindDrawerGroup()?.Color ?? EditorGUIProperty.DefaultColor;
        }

    }
}
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ScenarioTableItem : TreeViewItem
    {
        internal ScenarioElement element { get; }

        internal ScenarioTableItem(int id, string path)
            : base(id)
        {
            element = new ScenarioElement(path);
        }

    }
}
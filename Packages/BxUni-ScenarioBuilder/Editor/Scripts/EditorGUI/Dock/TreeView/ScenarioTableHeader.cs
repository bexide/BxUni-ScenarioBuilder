using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ScenarioTableHeader : MultiColumnHeader
    {
        public ScenarioTableHeader()
            : this(GetColumns())
        {

        }

        ScenarioTableHeader(MultiColumnHeaderState.Column[] columns)
            : base(new MultiColumnHeaderState(columns))
        {

        }

        static MultiColumnHeaderState.Column[] GetColumns()
        {
            return new MultiColumnHeaderState.Column[]
            {
                new NameColumn(),
                new PathColumn(),
                new BytesColumn(),
                new LastTimeColumn(),
                new OpenButtonColumn(),
            };
        }
    }
}
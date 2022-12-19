//BeXide 2022-12-12
//by MurakamiKazuki

using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ScenarioTableHeader : MultiColumnHeader
    {
        internal ScenarioTableHeader()
            : this(GetColumns())
        {

        }

        ScenarioTableHeader(MultiColumnHeaderState.Column[] columns)
            : base(new MultiColumnHeaderState(columns))
        {

        }

        internal static MultiColumnHeaderState.Column[] GetColumns()
        {
            return new MultiColumnHeaderState.Column[]
            {
                new NameColumn(),
                new PathColumn(),
                new BytesColumn(),
                new ValidateColumn(),
                new LastTimeColumn(),
                new OpenButtonColumn(),
            };
        }
    }
}
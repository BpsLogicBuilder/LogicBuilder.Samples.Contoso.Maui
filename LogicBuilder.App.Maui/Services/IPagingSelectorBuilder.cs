using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Services
{
    public interface IPagingSelectorBuilder
    {
        Task<SelectorLambdaDescriptor> CreateSelector(int skip, string? searchText, string createPagingSelectorFlowName);
    }
}

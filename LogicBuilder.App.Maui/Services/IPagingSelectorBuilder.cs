using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IPagingSelectorBuilder
    {
        Task<SelectorLambdaDescriptor> CreateSelector(int skip, string? searchText, string createPagingSelectorFlowName);
    }
}

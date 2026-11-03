using LogicBuilder.App.Maui.Directives;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Services
{
    public interface IConditionalDirectiveBuilder<TConditionBase, TModel> : IConditionalDirectiveBuilder where TConditionBase : ConditionBase<TModel>, new()
    {
        List<TConditionBase> GetConditions();
    }

    public interface IConditionalDirectiveBuilder
    {
    }
}

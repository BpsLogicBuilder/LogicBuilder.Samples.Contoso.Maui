using AutoMapper;
using LogicBuilder.App.Maui.Directives;
using LogicBuilder.App.Maui.Directives.Factories;
using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Services
{
    public class ReloadIfConditionalDirectiveBuilder<TModel>(
        IDirectiveManagersFactory directiveManagersFactory,
        IMapper mapper,
        IFormGroupSettings formGroupSettings,
        IEnumerable<IFormField> properties,
        List<ReloadIf<TModel>>? parentList = null,
        string? parentName = null) : BaseConditionalDirectiveBuilder<ReloadIf<TModel>, TModel>(
            directiveManagersFactory,
            mapper,
            formGroupSettings,
            properties,
            parentList,
            parentName)
    {
    }
}

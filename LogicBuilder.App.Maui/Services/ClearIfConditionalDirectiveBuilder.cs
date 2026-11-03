using AutoMapper;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ClearIfConditionalDirectiveBuilder<TModel>(
        IDirectiveManagersFactory directiveManagersFactory,
        IMapper mapper,
        IFormGroupSettings formGroupSettings,
        IEnumerable<IFormField> properties,
        List<ClearIf<TModel>>? parentList = null,
        string? parentName = null) : BaseConditionalDirectiveBuilder<ClearIf<TModel>, TModel>(
            directiveManagersFactory,
            mapper,
            formGroupSettings,
            properties,
            parentList,
            parentName)
    {
    }
}

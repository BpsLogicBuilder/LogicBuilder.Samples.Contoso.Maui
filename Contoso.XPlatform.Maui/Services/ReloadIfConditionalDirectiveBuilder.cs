using AutoMapper;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ReloadIfConditionalDirectiveBuilder<TModel> : BaseConditionalDirectiveBuilder<ReloadIf<TModel>, TModel>
    {
        public ReloadIfConditionalDirectiveBuilder(
            IDirectiveManagersFactory directiveManagersFactory,
            IMapper mapper,
            IFormGroupSettings formGroupSettings,
            IEnumerable<IFormField> properties,
            List<ReloadIf<TModel>>? parentList = null,
            string? parentName = null) : base(
                directiveManagersFactory,
                mapper,
                formGroupSettings,
                properties,
                parentList,
                parentName)
        {
        }
    }
}

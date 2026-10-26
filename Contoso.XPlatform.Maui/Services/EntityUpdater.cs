using AutoMapper;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class EntityUpdater(IMapper mapper) : IEntityUpdater
    {
        private readonly IMapper mapper = mapper;

        public TModel ToModelObject<TModel>(ObservableCollection<IValidatable> properties, List<IFormItemSettingsDescriptor> fieldSettings, TModel? destination = null) where TModel : class
        {
            return properties.ToModelObject(this.mapper, fieldSettings, destination);
        }

        public object ToModelObject(Type modelType, ObservableCollection<IValidatable> properties, List<IFormItemSettingsDescriptor> fieldSettings, object? destination = null)
        {
            return properties.ToModelObject(modelType, this.mapper, fieldSettings, destination);
        }
    }
}

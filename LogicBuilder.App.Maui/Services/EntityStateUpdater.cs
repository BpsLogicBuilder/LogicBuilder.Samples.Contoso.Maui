using AutoMapper;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class EntityStateUpdater(IMapper mapper) : IEntityStateUpdater
    {
        private readonly IMapper mapper = mapper;

        public TModel GetUpdatedModel<TModel>(TModel? existingEntity, Dictionary<string, object?> existingAsDictionary, ObservableCollection<IValidatable> modifiedProperties, List<IFormItemSettingsDescriptor> fieldSettings)
        {
            Dictionary<string, object?> current = modifiedProperties.ValidatableListToObjectDictionary
            (
                mapper,
                fieldSettings
            );

            EntityMapper.UpdateEntityStates
            (
                existingAsDictionary,
                current,
                fieldSettings
            );

            if (existingEntity is null)
            {
                return mapper.Map<TModel>(current);
            }

            return mapper.Map(current, existingEntity);
        }
    }
}

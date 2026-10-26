using AutoMapper;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Directives
{
    internal partial class ReloadIfManager<TModel> : IReloadIfManager
    {
        public ReloadIfManager(IMapper mapper, IUiNotificationService uiNotificationService, IEnumerable<IFormField> currentProperties, List<ReloadIf<TModel>> conditions)
        {
            CurrentProperties = currentProperties;
            this.conditions = conditions;
            this.mapper = mapper;
            this.uiNotificationService = uiNotificationService;
            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(PropertyChanged);
        }

        private readonly IMapper mapper;
        private readonly List<ReloadIf<TModel>> conditions;
        private readonly IUiNotificationService uiNotificationService;
        private readonly IDisposable propertyChangedSubscription;

        public IEnumerable<IFormField> CurrentProperties { get; }
        private Dictionary<string, IFormField> CurrentPropertiesDictionary
            => CurrentProperties.ToDictionary(p => p.Name);

        public void Check(ReloadIf<TModel> condition)
        {
            DoCheck(CurrentPropertiesDictionary[condition.Field]);

            void DoCheck(IFormField currentField)
            {
                if (currentField is IHasItemsSource hasItemsSource)
                {
                    TModel entity = mapper.Map<TModel>(CurrentProperties.ToDictionary(p => p.Name, p => p.Value));
                    if
                    (
                        ShouldReload
                        (
                            entity,
                            condition.Evaluator
                        )
                    )
                    {
                        if (entity is null)
                            throw new ArgumentException($"{nameof(entity)}: {{48192C13-24F3-4C15-93D3-410610155783}}");

                        hasItemsSource.Reload(entity, typeof(TModel));
                    }
                }
            }

            bool ShouldReload(TModel entity, Expression<Func<TModel, bool>> evaluator)
                => new List<TModel> { entity }.AsQueryable().All(evaluator);
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
                DisposeSubscription(propertyChangedSubscription);
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void PropertyChanged(string fieldName)
        {
            conditions.ForEach
            (
                condition =>
                {
                    CheckIdFieldIsWatched(fieldName, condition);
                }
            );
        }

        private void CheckIdFieldIsWatched(string fieldName, ReloadIf<TModel> condition)
        {
            if (condition.DirectiveDefinition.FunctionName == nameof(Check))
            {
                if (condition.DirectiveDefinition.Arguments == null || condition.DirectiveDefinition.Arguments.Count == 0)
                    throw new ArgumentException($"{condition.DirectiveDefinition.Arguments}: 5CC8C779-D9B5-4CEC-9B2B-4D2AC8558E21");

                const string fieldsToWatch = "fieldsToWatch";
                if (!condition.DirectiveDefinition.Arguments.TryGetValue(fieldsToWatch, out DirectiveArgumentDescriptor? fieldsToWatchDescriptor))
                    throw new ArgumentException($"{fieldsToWatch}: 2202BFA2-C2B4-4A93-975B-BB66DC975173");
                if (fieldsToWatchDescriptor.Value is not IEnumerable<string>)
                    throw new ArgumentException($"{fieldsToWatchDescriptor}: 923F2F02-F65D-4F88-9612-DA5991FA0A24");

                if (
                        new HashSet<string>
                        (
                            ((IEnumerable<string>)fieldsToWatchDescriptor.Value).Select(f => GetFieldName(f, condition.ParentField))
                        ).Contains(fieldName)
                  )
                {
                    Check(condition);
                }
            }
            else
            {
                throw new ArgumentException($"{condition.DirectiveDefinition.FunctionName}: CF744C7B-399B-4332-8431-62758B473248");
            }
        }

        private static void DisposeSubscription(IDisposable subscription)
        {
            subscription?.Dispose();
        }

        private static string GetFieldName(string field, string? parentName)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}

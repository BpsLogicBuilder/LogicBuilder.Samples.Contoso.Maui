using AutoMapper;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Directives
{
    internal partial class ValidateIfManager<TModel> : IValidateIfManager
    {
        public ValidateIfManager(IMapper mapper, IUiNotificationService uiNotificationService, IEnumerable<IValidatable> currentProperties, List<ValidateIf<TModel>> conditions)
        {
            CurrentProperties = currentProperties;
            this.conditions = conditions;
            this.mapper = mapper;
            this.uiNotificationService = uiNotificationService;
            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(PropertyChanged);
        }

        private readonly IMapper mapper;
        private readonly List<ValidateIf<TModel>> conditions;
        private readonly IUiNotificationService uiNotificationService;
        private readonly IDisposable propertyChangedSubscription;

        public IEnumerable<IValidatable> CurrentProperties { get; }
        private Dictionary<string, IValidatable> CurrentPropertiesDictionary
            => CurrentProperties.ToDictionary(p => p.Name);

        public void Check(ValidateIf<TModel> condition)
        {
            DoCheck(CurrentPropertiesDictionary[condition.Field]);

            void DoCheck(IValidatable currentValidatable)
            {
                HashSet<IValidationRule> existingRules = [.. currentValidatable.Validations];
                TModel entity = mapper.Map<TModel>(CurrentProperties.ToDictionary(p => p.Name, p => p.Value));
                if (CanValidate(entity, condition.Evaluator))
                {
                    if (!existingRules.Contains(condition.Validator))
                    {
                        currentValidatable.Validations.Add(condition.Validator);
                        currentValidatable.Validate();
                    }
                }
                else
                {
                    if (existingRules.Contains(condition.Validator))
                    {
                        currentValidatable.Validations.Remove(condition.Validator);
                        currentValidatable.Validate();
                    }
                }
            }

            bool CanValidate(TModel entity, Expression<Func<TModel, bool>> evaluator)
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
                    CheckIfFieldIsWatched(fieldName, condition);
                }
            );
        }

        private void CheckIfFieldIsWatched(string fieldName, ValidateIf<TModel> condition)
        {
            if (condition.DirectiveDefinition.FunctionName == nameof(Check))
            {
                if ((condition.DirectiveDefinition.Arguments?.Count ?? 0) == 0)
                    throw new ArgumentException($"{condition.DirectiveDefinition.Arguments}: F1DA1B2F-9397-439B-BC5B-AEFB85A9E4E5");

                const string fieldsToWatch = "fieldsToWatch";
                if (condition.DirectiveDefinition.Arguments == null || !condition.DirectiveDefinition.Arguments.TryGetValue(fieldsToWatch, out DirectiveArgumentDescriptor? fieldsToWatchDescriptor))
                    throw new ArgumentException($"{fieldsToWatch}: 36940976-0DAD-4171-A181-445216EC0A26");
                if (fieldsToWatchDescriptor.Value is not IEnumerable<string>)
                    throw new ArgumentException($"{fieldsToWatchDescriptor}: 1B131DAA-276A-438E-88A8-031AF504E421");

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
                throw new ArgumentException($"{condition.DirectiveDefinition.FunctionName}: 8720D414-5352-4401-97C9-D6D7E9454292");
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

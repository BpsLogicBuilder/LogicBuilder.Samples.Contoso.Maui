using AutoMapper;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Directives;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Services
{
    public class ValidateIfConditionalDirectiveBuilder<TModel>(IDirectiveManagersFactory directiveManagersFactory, IMapper mapper, IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties, List<ValidateIf<TModel>>? parentList = null, string? parentName = null) : IConditionalDirectiveBuilder<ValidateIf<TModel>, TModel>
    {
        private readonly IDirectiveManagersFactory directiveManagersFactory = directiveManagersFactory;
        private readonly IMapper mapper = mapper;
        private readonly string? parentName = parentName;
        private readonly List<ValidateIf<TModel>>? parentList = parentList;
        private readonly IFormGroupSettings formGroupSettings = formGroupSettings;
        private readonly IEnumerable<IValidatable> properties = properties;
        const string PARAMETERS_KEY = "parameters";

        public List<ValidateIf<TModel>> GetConditions()
        {
            Dictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);

            List<ValidateIf<TModel>> conditions = formGroupSettings.ConditionalDirectives?.Aggregate(parentList ?? [], (list, kvp) =>
            {
                kvp.Value.ForEach
                (
                    descriptor =>
                    {
                        if (descriptor.Definition.ClassName != nameof(ValidateIf<>))
                            return;

                        const string validationsToWatchKey = "validationsToWatch";
                        if (descriptor.Definition.Arguments == null || !descriptor.Definition.Arguments.TryGetValue(validationsToWatchKey, out DirectiveArgumentDescriptor? validationsToWatchDescriptor))
                            throw new ArgumentException($"{validationsToWatchKey}: A5B39033-62C9-4B5D-A800-754054169502");
                        if (validationsToWatchDescriptor.Value is not IEnumerable<string>)
                            throw new ArgumentException($"{validationsToWatchDescriptor}: 60E64249-A275-4992-8444-9DC85DF108B9");

                        HashSet<string> validationsToWatch = [.. (IEnumerable<string>)validationsToWatchDescriptor.Value];

                        IValidatable validatable = propertiesDictionary[GetFieldName(kvp.Key)];

                        validatable.Validations.ForEach
                        (
                            AddValidationToWatch(list, kvp, descriptor, validationsToWatch)
                        );
                    }
                );

                return list;
            }) ?? [];

            formGroupSettings.FieldSettings.ForEach(d => AddConditions(d, conditions));

            return conditions;

            
        }

        void AddConditions(IFormItemSettingsDescriptor descriptor, List<ValidateIf<TModel>> conditions)
        {
            if (descriptor is FormGroupBoxSettingsDescriptor groupBox)
            {
                groupBox.FieldSettings.ForEach(d => AddConditions(d, conditions));
                return;
            }

            if (descriptor is not FormGroupSettingsDescriptor childForm)
                return;

            if ((childForm.FormGroupTemplate?.TemplateName) != FromGroupTemplateNames.InlineFormGroupTemplate)
                return;

            conditions = this.directiveManagersFactory.GetDirectiveConditionsBuilder<ValidateIf<TModel>, TModel>
            (
                childForm,
                properties,
                conditions,
                GetFieldName(childForm.Field)
            ).GetConditions();
        }

        private Action<Validators.IValidationRule> AddValidationToWatch(List<ValidateIf<TModel>> list, KeyValuePair<string, List<DirectiveDescriptor>> kvp, DirectiveDescriptor descriptor, HashSet<string> validationsToWatch)
        {
            return validationRule =>
            {
                if (!validationsToWatch.Contains(validationRule.ClassName))
                    return;

                list.Add
                (
                    new ValidateIf<TModel>
                    {
                        Field = GetFieldName(kvp.Key),
                        ParentField = this.parentName,
                        Validator = validationRule,
                        Evaluator = (Expression<Func<TModel, bool>>)mapper.Map<FilterLambdaOperator>
                        (
                            descriptor.Condition,
                            opts => opts.Items[PARAMETERS_KEY] = new Dictionary<string, ParameterExpression>()
                        ).Build(),
                        DirectiveDefinition = descriptor.Definition
                    }
                );
            };
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}

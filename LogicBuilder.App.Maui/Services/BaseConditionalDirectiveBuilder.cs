using AutoMapper;
using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Directives;
using LogicBuilder.App.Maui.Directives.Factories;
using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LogicBuilder.App.Maui.Services
{
    abstract public class BaseConditionalDirectiveBuilder<TConditionBase, TModel>(IDirectiveManagersFactory directiveManagersFactory, IMapper mapper, IFormGroupSettings formGroupSettings, IEnumerable<IFormField> properties, List<TConditionBase>? parentList = null, string? parentName = null) : IConditionalDirectiveBuilder<TConditionBase, TModel> where TConditionBase : ConditionBase<TModel>, new()
    {
        private readonly IDirectiveManagersFactory directiveManagersFactory = directiveManagersFactory;
        private readonly IFormGroupSettings formGroupSettings = formGroupSettings;
        private readonly IEnumerable<IFormField> properties = properties;
        private readonly IMapper mapper = mapper;
        private readonly List<TConditionBase>? parentList = parentList;
        private readonly string? parentName = parentName;
        const string PARAMETERS_KEY = "parameters";

        public List<TConditionBase> GetConditions()
        {
            List<TConditionBase> conditions = formGroupSettings.ConditionalDirectives?.Aggregate(parentList ?? [], (list, kvp) =>
            {
                kvp.Value.ForEach
                (
                    descriptor =>
                    {
                        if ($"{descriptor.Definition.ClassName}`1" != typeof(TConditionBase).Name)
                            return;

                        list.Add
                        (
                            new TConditionBase
                            {
                                Field = GetFieldName(kvp.Key),
                                ParentField = this.parentName,
                                Evaluator = (Expression<Func<TModel, bool>>)mapper.Map<FilterLambdaOperator>
                                (
                                    descriptor.Condition,
                                    opts => opts.Items[PARAMETERS_KEY] = new Dictionary<string, ParameterExpression>()
                                ).Build(),
                                DirectiveDefinition = descriptor.Definition
                            }
                        );
                    }
                );

                return list;
            }) ?? [];

            formGroupSettings.FieldSettings.ForEach(AddConditions);

            return conditions;

            void AddConditions(IFormItemSettingsDescriptor descriptor)
            {
                if (descriptor is FormGroupBoxSettingsDescriptor groupBox)
                {
                    groupBox.FieldSettings.ForEach(AddConditions);
                    return;
                }

                if (descriptor is not FormGroupSettingsDescriptor childForm)
                    return;

                if ((childForm.FormGroupTemplate?.TemplateName) != FromGroupTemplateNames.InlineFormGroupTemplate)
                    return;

                var helper = directiveManagersFactory.GetDirectiveConditionsBuilder<TConditionBase, TModel>
                (
                    childForm,
                    properties,
                    conditions,
                    GetFieldName(childForm.Field)
                );

                conditions = helper.GetConditions();
            }
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";

    }
}

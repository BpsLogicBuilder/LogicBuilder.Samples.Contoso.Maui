using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;

namespace LogicBuilder.App.Maui.Test.ScreenDescriptors
{
    internal static class ListFormDescriptors
    {
        internal static ListFormSettingsDescriptor AboutForm = new
        (
            title: "About",
            modelType: typeof(LookUpsModel).AssemblyQualifiedName!,
            loadingIndicatorText: "Loading ...",
            itemTemplateName: "TextDetailTemplate",
            bindings: new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                (
                    name: "Text",
                    property: "DateTimeValue",
                    title: "DateTimeValue",
                    stringFormat: "Enrollment Date: {0:MM/dd/yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    fieldTypeSource: null
                )
            }.ToDictionary(i => i.Name),
            fieldsSelector: new SelectorLambdaDescriptor
            (
                selector: new SelectDescriptor
                (
                    sourceOperand: new OrderByDescriptor
                    (
                        sourceOperand: new GroupByDescriptor
                        (
                            sourceOperand: new ParameterDescriptor("q"),
                            selectorBody: new MemberSelectorDescriptor
                            (
                                memberFullName: "EnrollmentDate",
                                sourceOperand: new ParameterDescriptor("item")
                            ),
                            selectorParameterName: "item"
                        ),
                        sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                        selectorBody: new MemberSelectorDescriptor
                        (
                            memberFullName: "Key",
                            sourceOperand: new ParameterDescriptor("group")
                        ),
                        selectorParameterName: "group"
                    ),
                    selectorBody: new MemberInitDescriptor
                    (
                        memberBindings: new Dictionary<string, DescriptorBase>
                        {
                            ["DateTimeValue"] = new MemberSelectorDescriptor
                            (
                                memberFullName: "Key",
                                sourceOperand: new ParameterDescriptor("sel")
                            ),
                            ["NumericValue"] = new ConvertDescriptor
                            (
                                sourceOperand: new CountDescriptor
                                (
                                    sourceOperand: new AsQueryableDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor("sel")
                                    )
                                ),
                                type: typeof(double?).FullName!
                            )
                        },
                        newType: typeof(LookUpsModel).AssemblyQualifiedName
                    ),
                    selectorParameterName: "sel"
                ),
                sourceElementType: typeof(IQueryable<StudentModel>).AssemblyQualifiedName!,
                parameterName: "$it",
                bodyType:typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName
            ),
            requestDetails: new RequestDetailsDescriptor
            (
                dataSourceUrl: "api/List/GetList",
                modelType: typeof(StudentModel).AssemblyQualifiedName!,
                dataType: typeof(Student).AssemblyQualifiedName!,
                modelReturnType: typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName!,
                dataReturnType: typeof(IQueryable<LookUps>).AssemblyQualifiedName!
            )
        );
    }
}

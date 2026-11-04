using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Maui.Test.ScreenDescriptors
{
    internal static class SearchFormDescriptors
    {
        internal static SearchFormSettingsDescriptor SudentsForm = new
        (
            title: "About",
            modelType: typeof(LookUpsModel).AssemblyQualifiedName!,
            loadingIndicatorText: "Loading ...",
            itemTemplateName: "TextDetailTemplate",
            filterPlaceholder: "filter",
            createPagingSelectorFlowName: "paging_selector_students",
            bindings: new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                (
                    name: "Text",
                    property: "DateTimeValue",
                    title: "DateTimeValue",
                    stringFormat: "Enrollment Date: {0:MM/dd/yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor
                    (
                        "TextTemplate"
                    ),
                    fieldTypeSource: null
                )
            }.ToDictionary(i => i.Name),
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

using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;

namespace LogicBuilder.App.Maui.Test.ScreenDescriptors
{
    internal static class ReadOnlyDescriptors
    {
        internal static DataFormSettingsDescriptor InstructorFormWithInlineOfficeAssignment = new
        (
            title: "Instructor",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Instructor/GetSingle",
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                modelType: "",
                dataType: "",
                filter: null,
                selectExpandDefinition: null
            ),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "ID",
                    type: "System.Int32",
                    title: "ID",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "HiddenTemplate" ),
                    placeholder: "",
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "FirstName",
                    type: "System.String",
                    title: "First Name",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "LastName",
                    type: "System.String",
                    title: "Last Name",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "HireDate",
                    type: "System.DateTime",
                    title: "Hire Date",
                    stringFormat: "{0:MM/dd/yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new FormGroupSettingsDescriptor
                (
                    field: "OfficeAssignment",
                    modelType: typeof(OfficeAssignmentModel).AssemblyQualifiedName!,
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Location",
                            type: "System.String",
                            title: "Location",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    formGroupTemplate: new FormGroupTemplateDescriptor
                    (
                        templateName: "InlineFormGroupTemplate"
                    ),
                    title: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    placeholder: "",
                    validationMessages: [],
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null
                ),
                new MultiSelectFormControlSettingsDescriptor
                (
                    keyFields: ["CourseID"],
                    field: "Courses",
                    title: "Courses",
                    type: typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName!,
                    multiSelectTemplate:  new MultiSelectTemplateDescriptor
                    (
                        templateName: "MultiSelectTemplate",
                        placeholderText: "(Courses)",
                        loadingIndicatorText: "Loading ...",
                        modelType: typeof(CourseAssignmentModel).AssemblyQualifiedName!,
                        textField: "CourseTitle",
                        valueField: "CourseID",
                        textAndValueSelector: new SelectorLambdaDescriptor
                        (
                            selector: new SelectDescriptor
                            (
                                sourceOperand: new OrderByDescriptor
                                (
                                    sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                    selectorBody: new MemberSelectorDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                        memberFullName: "Title"
                                    ),
                                    sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    selectorParameterName: "d"
                                ),
                                selectorBody: new MemberInitDescriptor
                                (
                                    memberBindings: new Dictionary<string, DescriptorBase>
                                    {
                                        ["CourseID"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "CourseID"
                                        ),
                                        ["CourseTitle"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "Title"
                                        )
                                    },
                                    newType: typeof(CourseAssignmentModel).AssemblyQualifiedName
                                ),
                                selectorParameterName: "s"
                            ),
                            sourceElementType: typeof(IQueryable<CourseModel>).AssemblyQualifiedName!,
                            parameterName: "$it",
                            bodyType: typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName
                        ),
                        requestDetails: new RequestDetailsDescriptor
                        (
                            dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                            modelType: typeof(CourseModel).AssemblyQualifiedName!,
                            dataType: typeof(Course).AssemblyQualifiedName!,
                            modelReturnType: typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName!,
                            dataReturnType: typeof(IEnumerable<CourseAssignment>).AssemblyQualifiedName!
                        ),
                        fieldTypeSource: null
                    ),
                    placeholder: "",
                    stringFormat: "",
                    validationSetting: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            validationMessages: [],
            formType: FormType.Detail,
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );

        internal static DataFormSettingsDescriptor InstructorFormWithPopupOfficeAssignment = new
        (
            title: "Instructor",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Instructor/GetSingle",
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                modelType: "",
                dataType: "",
                filter: null,
                selectExpandDefinition: null
            ),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "ID",
                    type: "System.Int32",
                    title: "ID",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "HiddenTemplate" ),
                    placeholder: "",
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "FirstName",
                    type: "System.String",
                    title: "First Name",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "LastName",
                    type: "System.String",
                    title: "Last Name",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "HireDate",
                    type: "System.DateTime",
                    title: "Hire Date",
                    stringFormat: "{0:MM/dd/yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new FormGroupSettingsDescriptor
                (
                    field: "OfficeAssignment",
                    modelType: typeof(OfficeAssignmentModel).AssemblyQualifiedName!,
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Location",
                            type: "System.String",
                            title: "Location",
                            stringFormat: "{0}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    formGroupTemplate: new FormGroupTemplateDescriptor
                    (
                        templateName: "PopupFormGroupTemplate"
                    ),
                    title: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    placeholder: "",
                    validationMessages: [],
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null
                ),
                new MultiSelectFormControlSettingsDescriptor
                (
                    keyFields: ["CourseID"],
                    field: "Courses",
                    title: "Courses",
                    type: typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName!,
                    multiSelectTemplate:  new MultiSelectTemplateDescriptor
                    (
                        templateName: "MultiSelectTemplate",
                        placeholderText: "(Courses)",
                        loadingIndicatorText: "Loading ...",
                        modelType: typeof(CourseAssignmentModel).AssemblyQualifiedName!,
                        textField: "CourseTitle",
                        valueField: "CourseID",
                        textAndValueSelector: new SelectorLambdaDescriptor
                        (
                            selector: new SelectDescriptor
                            (
                                sourceOperand: new OrderByDescriptor
                                (
                                    sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                    selectorBody: new MemberSelectorDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                        memberFullName: "Title"
                                    ),
                                    sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    selectorParameterName: "d"
                                ),
                                selectorBody: new MemberInitDescriptor
                                (
                                    memberBindings: new Dictionary<string, DescriptorBase>
                                    {
                                        ["CourseID"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "CourseID"
                                        ),
                                        ["CourseTitle"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "Title"
                                        )
                                    },
                                    newType: typeof(CourseAssignmentModel).AssemblyQualifiedName
                                ),
                                selectorParameterName: "s"
                            ),
                            sourceElementType: typeof(IQueryable<CourseModel>).AssemblyQualifiedName!,
                            parameterName: "$it",
                            bodyType: typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName
                        ),
                        requestDetails: new RequestDetailsDescriptor
                        (
                            dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                            modelType: typeof(CourseModel).AssemblyQualifiedName!,
                            dataType: typeof(Course).AssemblyQualifiedName!,
                            modelReturnType: typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName!,
                            dataReturnType: typeof(IEnumerable<CourseAssignment>).AssemblyQualifiedName!
                        ),
                        fieldTypeSource: null
                    ),
                    placeholder: "",
                    stringFormat: "",
                    validationSetting: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            validationMessages: [],
            formType: FormType.Detail,
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );

        internal static DataFormSettingsDescriptor DepartmentForm = new
        (
            title: "Department",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Department/GetSingle",
                modelType: typeof(DepartmentModel).AssemblyQualifiedName!,
                dataType: typeof(Department).AssemblyQualifiedName!,
                filter: new FilterLambdaDescriptor
                (
                    filterBody: new EqualsBinaryDescriptor
                    (
                        left: new MemberSelectorDescriptor
                        (
                            sourceOperand: new ParameterDescriptor ( parameterName: "f" ),
                            memberFullName: "DepartmentID"
                        ),
                        right: new ConstantDescriptor ( type: typeof(int).FullName, constantValue: 1 )
                    ),
                    sourceElementType: typeof(DepartmentModel).AssemblyQualifiedName!,
                    parameterName: "f"
                ),
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                selectExpandDefinition: null
            ),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "DepartmentID",
                    type: "System.Int32",
                    title: "ID",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "HiddenTemplate" ),
                    placeholder: "",
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Budget",
                    type: "System.Decimal",
                    title: "Budget",
                    stringFormat: "{0:F2}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new DropdownSelectorControlSettingsDescriptor
                (
                    field: "InstructorID",
                    type: "System.Int32",
                    title: "Administrator",
                    stringFormat: "{0}",
                    dropDownTemplate: new DropDownTemplateDescriptor
                    (
                        templateName: "PickerTemplate",
                        titleText: "Select Administrator:",
                        loadingIndicatorText: "Loading ...",
                        textField: "FullName",
                        valueField: "ID",
                        textAndValueSelector: new SelectorLambdaDescriptor
                        (
                            selector: new SelectDescriptor
                            (
                                sourceOperand: new OrderByDescriptor
                                (
                                    sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                    selectorBody: new MemberSelectorDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                        memberFullName: "FullName"
                                    ),
                                    sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    selectorParameterName: "d"
                                ),
                                selectorBody: new MemberInitDescriptor
                                (
                                    memberBindings: new Dictionary<string, DescriptorBase>
                                    {
                                        ["ID"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "ID"
                                        ),
                                        ["FirstName"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "FirstName"
                                        ),
                                        ["LastName"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "LastName"
                                        ),
                                        ["FullName"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                            memberFullName: "FullName"
                                        )
                                    },
                                    newType: typeof(InstructorModel).AssemblyQualifiedName
                                ),
                                selectorParameterName: "s"
                            ),
                            sourceElementType: typeof(IQueryable<InstructorModel>).AssemblyQualifiedName!,
                            parameterName: "$it",
                            bodyType: typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                        ),
                        requestDetails: new RequestDetailsDescriptor
                        (
                            dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                            modelType: typeof(InstructorModel).AssemblyQualifiedName!,
                            dataType: typeof(Instructor).AssemblyQualifiedName!,
                            modelReturnType: typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName!,
                            dataReturnType: typeof(IEnumerable<Instructor>).AssemblyQualifiedName!
                        ),
                        reloadItemsFlowName: null,
                        fieldTypeSource: null
                    ),
                    placeholder: "",
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Name",
                    type: "System.String",
                    title: "Name",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "StartDate",
                    type: "System.DateTime",
                    title: "Start Date",
                    stringFormat: "{0:MM/dd/yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new FormGroupArraySettingsDescriptor
                (
                    field: "Courses",
                    formGroupTemplate: new FormGroupTemplateDescriptor
                    (
                        templateName: "FormGroupArrayTemplate"
                    ),
                    formsCollectionDisplayTemplate: new FormsCollectionDisplayTemplateDescriptor
                    (
                        templateName: "TextDetailTemplate",
                        bindings: new List<ItemBindingDescriptor>
                        {
                            new TextItemBindingDescriptor
                            (
                                name: "Text",
                                property: "DepartmentName",
                                stringFormat: "{0}",
                                textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                title: "",
                                fieldTypeSource: null
                            ),
                            new TextItemBindingDescriptor
                            (
                                name: "Detail",
                                property: "Credits",
                                stringFormat: "{0}",
                                textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                title: "",
                                fieldTypeSource: null
                            )
                        }.ToDictionary(b => b.Name)
                    ),
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "CourseID",
                            type: "System.Int32",
                            title: "Course",
                            stringFormat: "{0}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "Credits",
                            type: "System.Int32",
                            title: "Credits",
                            stringFormat: "{0}",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select credits:",
                                loadingIndicatorText: "Loading ...",
                                textField: "Text",
                                valueField: "NumericValue",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new WhereDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                                filterBody: new EqualsBinaryDescriptor
                                                (
                                                    left: new MemberSelectorDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                        memberFullName: "ListName"
                                                    ),
                                                    right: new ConstantDescriptor
                                                    (
                                                        constantValue: "Credits",
                                                        type: typeof(string).AssemblyQualifiedName
                                                    )
                                                ),
                                                filterParameterName: "l"
                                            ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                memberFullName: "NumericValue"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            selectorParameterName: "l"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["NumericValue"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "NumericValue"
                                                ),
                                                ["Text"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "Text"
                                                )
                                            },
                                            newType: typeof(LookUpsModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "s"
                                    ),
                                    sourceElementType: typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(LookUpsModel).AssemblyQualifiedName!,
                                    dataType: typeof(LookUps).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<LookUps>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            placeholder: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "DepartmentID",
                            type: "System.Int32",
                            title: "Department",
                            stringFormat: "{0}",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select Department:",
                                loadingIndicatorText: "Loading ...",
                                textField: "Name",
                                valueField: "DepartmentID",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                memberFullName: "Name"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            selectorParameterName: "d"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["DepartmentID"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                    memberFullName: "DepartmentID"
                                                ),
                                                ["Name"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                    memberFullName: "Name"
                                                )
                                            },
                                            newType: typeof(DepartmentModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "d"
                                    ),
                                    sourceElementType: typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(DepartmentModel).AssemblyQualifiedName!,
                                    dataType: typeof(Department).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<Department>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            placeholder: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Title",
                            type: "System.String",
                            title: "Title",
                            stringFormat: "{0}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    type: typeof(ICollection<CourseModel>).AssemblyQualifiedName!,
                    modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    keyFields: ["CourseID"],
                    title: "",
                    placeholder: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    validationMessages: [],
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            validationMessages: [],
            formType: FormType.Detail,
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );

        internal static DataFormSettingsDescriptor DepartmentFormWithAllItemsGrouped = new
        (
            title: "Department",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Department/GetSingle",
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                modelType: "",
                dataType: "",
                filter: null,
                selectExpandDefinition: null
            ),
            fieldSettings:
            [
                new FormGroupBoxSettingsDescriptor
                (
                    groupHeader: "GroupOne",
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "DepartmentID",
                            type: "System.Int32",
                            title: "ID",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "HiddenTemplate" ),
                            placeholder: "",
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Budget",
                            type: "System.Decimal",
                            title: "Budget",
                            stringFormat: "{0:F2}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "InstructorID",
                            type: "System.Int32",
                            title: "Administrator",
                            stringFormat: "{0}",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select Administrator:",
                                loadingIndicatorText: "Loading ...",
                                textField: "FullName",
                                valueField: "ID",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                memberFullName: "FullName"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            selectorParameterName: "d"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["ID"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "ID"
                                                ),
                                                ["FirstName"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "FirstName"
                                                ),
                                                ["LastName"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "LastName"
                                                ),
                                                ["FullName"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "FullName"
                                                )
                                            },
                                            newType: typeof(InstructorModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "s"
                                    ),
                                    sourceElementType: typeof(IQueryable<InstructorModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(InstructorModel).AssemblyQualifiedName!,
                                    dataType: typeof(Instructor).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<Instructor>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            placeholder: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                    ],
                    headerBindings: null,
                    isHidden: false
                ),
                new FormGroupBoxSettingsDescriptor
                (
                    groupHeader: "GroupTwo",
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Name",
                            type: "System.String",
                            title: "Name",
                            stringFormat: "{0}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "StartDate",
                            type: "System.DateTime",
                            title: "Start Date",
                            stringFormat: "{0:MM/dd/yyyy}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new FormGroupArraySettingsDescriptor
                        (
                            field: "Courses",
                            formGroupTemplate: new FormGroupTemplateDescriptor
                            (
                                templateName: "FormGroupArrayTemplate"
                            ),
                            formsCollectionDisplayTemplate: new FormsCollectionDisplayTemplateDescriptor
                            (
                                templateName: "TextDetailTemplate",
                                bindings: new List<ItemBindingDescriptor>
                                {
                                    new TextItemBindingDescriptor
                                    (
                                        name: "Text",
                                        property: "DepartmentName",
                                        stringFormat: "{0}",
                                        textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                        title: "",
                                        fieldTypeSource: null
                                    ),
                                    new TextItemBindingDescriptor
                                    (
                                        name: "Detail",
                                        property: "StartDate",
                                        stringFormat: "{0:MMMM dd, yyyy}",
                                        textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                        title: "",
                                        fieldTypeSource: null
                                    )
                                }.ToDictionary(b => b.Name)
                            ),
                            fieldSettings:
                            [
                                new InputFieldControlSettingsDescriptor
                                (
                                    field: "CourseID",
                                    type: "System.Int32",
                                    title: "Course",
                                    stringFormat: "{0}",
                                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                    placeholder: "",
                                    updateOnlyTextTemplate: null,
                                    validationSetting: null,
                                    fieldTypeSource: null
                                ),
                                new DropdownSelectorControlSettingsDescriptor
                                (
                                    field: "Credits",
                                    type: "System.Int32",
                                    title: "Credits",
                                    stringFormat: "{0}",
                                    dropDownTemplate: new DropDownTemplateDescriptor
                                    (
                                        templateName: "PickerTemplate",
                                        titleText: "Select credits:",
                                        loadingIndicatorText: "Loading ...",
                                        textField: "Text",
                                        valueField: "NumericValue",
                                        textAndValueSelector: new SelectorLambdaDescriptor
                                        (
                                            selector: new SelectDescriptor
                                            (
                                                sourceOperand: new OrderByDescriptor
                                                (
                                                    sourceOperand: new WhereDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                                        filterBody: new EqualsBinaryDescriptor
                                                        (
                                                            left: new MemberSelectorDescriptor
                                                            (
                                                                sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                                memberFullName: "ListName"
                                                            ),
                                                            right: new ConstantDescriptor
                                                            (
                                                                constantValue: "Credits",
                                                                type: typeof(string).AssemblyQualifiedName
                                                            )
                                                        ),
                                                        filterParameterName: "l"
                                                    ),
                                                    selectorBody: new MemberSelectorDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                        memberFullName: "NumericValue"
                                                    ),
                                                    sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                                    selectorParameterName: "l"
                                                ),
                                                selectorBody: new MemberInitDescriptor
                                                (
                                                    memberBindings: new Dictionary<string, DescriptorBase>
                                                    {
                                                        ["NumericValue"] = new MemberSelectorDescriptor
                                                        (
                                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                            memberFullName: "NumericValue"
                                                        ),
                                                        ["Text"] = new MemberSelectorDescriptor
                                                        (
                                                            sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                            memberFullName: "Text"
                                                        )
                                                    },
                                                    newType: typeof(LookUpsModel).AssemblyQualifiedName
                                                ),
                                                selectorParameterName: "s"
                                            ),
                                            sourceElementType: typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName!,
                                            parameterName: "$it",
                                            bodyType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                        ),
                                        requestDetails: new RequestDetailsDescriptor
                                        (
                                            dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                            modelType: typeof(LookUpsModel).AssemblyQualifiedName!,
                                            dataType: typeof(LookUps).AssemblyQualifiedName!,
                                            modelReturnType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName!,
                                            dataReturnType: typeof(IEnumerable<LookUps>).AssemblyQualifiedName!
                                        ),
                                        reloadItemsFlowName: null,
                                        fieldTypeSource: null
                                    ),
                                    placeholder: "",
                                    validationSetting: null,
                                    fieldTypeSource: null
                                ),
                                new DropdownSelectorControlSettingsDescriptor
                                (
                                    field: "DepartmentID",
                                    type: "System.Int32",
                                    title: "Department",
                                    stringFormat: "{0}",
                                    dropDownTemplate: new DropDownTemplateDescriptor
                                    (
                                        templateName: "PickerTemplate",
                                        titleText: "Select Department:",
                                        loadingIndicatorText: "Loading ...",
                                        textField: "Name",
                                        valueField: "DepartmentID",
                                        textAndValueSelector: new SelectorLambdaDescriptor
                                        (
                                            selector: new SelectDescriptor
                                            (
                                                sourceOperand: new OrderByDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                                    selectorBody: new MemberSelectorDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                        memberFullName: "Name"
                                                    ),
                                                    sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                                    selectorParameterName: "d"
                                                ),
                                                selectorBody: new MemberInitDescriptor
                                                (
                                                    memberBindings: new Dictionary<string, DescriptorBase>
                                                    {
                                                        ["DepartmentID"] = new MemberSelectorDescriptor
                                                        (
                                                            sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                            memberFullName: "DepartmentID"
                                                        ),
                                                        ["Name"] = new MemberSelectorDescriptor
                                                        (
                                                            sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                            memberFullName: "Name"
                                                        )
                                                    },
                                                    newType: typeof(DepartmentModel).AssemblyQualifiedName
                                                ),
                                                selectorParameterName: "d"
                                            ),
                                            sourceElementType: typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName!,
                                            parameterName: "$it",
                                            bodyType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                        ),
                                        requestDetails: new RequestDetailsDescriptor
                                        (
                                            dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                            modelType: typeof(DepartmentModel).AssemblyQualifiedName!,
                                            dataType: typeof(Department).AssemblyQualifiedName!,
                                            modelReturnType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName!,
                                            dataReturnType: typeof(IEnumerable<Department>).AssemblyQualifiedName!
                                        ),
                                        reloadItemsFlowName: null,
                                        fieldTypeSource: null
                                    ),
                                    placeholder: "",
                                    validationSetting: null,
                                    fieldTypeSource: null
                                ),
                                new InputFieldControlSettingsDescriptor
                                (
                                    field: "Title",
                                    type: "System.String",
                                    title: "Title",
                                    stringFormat: "{0}",
                                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                    placeholder: "",
                                    updateOnlyTextTemplate: null,
                                    validationSetting: null,
                                    fieldTypeSource: null
                                )
                            ],
                            type: typeof(ICollection<CourseModel>).AssemblyQualifiedName!,
                            modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            keyFields: ["CourseID"],
                            title: "",
                            placeholder: "",
                            validFormControlText: "",
                            invalidFormControlText: "",
                            validationMessages: [],
                            conditionalDirectives: null,
                            headerBindings: null,
                            fieldTypeSource: null,
                            listElementTypeSource: null
                        )
                    ],
                    headerBindings: null,
                    isHidden: false
                )
            ],
            modelType: "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            validationMessages: [],
            formType: FormType.Detail,
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );

        internal static DataFormSettingsDescriptor DepartmentFormWithSomeItemsGrouped = new
        (
            title: "Department",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Department/GetSingle",
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                modelType: "",
                dataType: "",
                filter: null,
                selectExpandDefinition: null
            ),
            fieldSettings:
            [
                new FormGroupBoxSettingsDescriptor
                (
                    groupHeader: "GroupOne",
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "DepartmentID",
                            type: "System.Int32",
                            title: "ID",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "HiddenTemplate" ),
                            placeholder: "",
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Budget",
                            type: "System.Decimal",
                            title: "Budget",
                            stringFormat: "{0:F2}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "InstructorID",
                            type: "System.Int32",
                            title: "Administrator",
                            stringFormat: "{0}",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select Administrator:",
                                loadingIndicatorText: "Loading ...",
                                textField: "FullName",
                                valueField: "ID",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                memberFullName: "FullName"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            selectorParameterName: "d"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["ID"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "ID"
                                                ),
                                                ["FirstName"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "FirstName"
                                                ),
                                                ["LastName"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "LastName"
                                                ),
                                                ["FullName"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "FullName"
                                                )
                                            },
                                            newType: typeof(InstructorModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "s"
                                    ),
                                    sourceElementType: typeof(IQueryable<InstructorModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(InstructorModel).AssemblyQualifiedName!,
                                    dataType: typeof(Instructor).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<Instructor>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            placeholder: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                    ],
                    headerBindings: null,
                    isHidden: false
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Name",
                    type: "System.String",
                    title: "Name",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "StartDate",
                    type: "System.DateTime",
                    title: "Start Date",
                    stringFormat: "{0:MM/dd/yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    placeholder: "",
                    updateOnlyTextTemplate: null,
                    validationSetting: null,
                    fieldTypeSource: null
                ),
                new FormGroupArraySettingsDescriptor
                (
                    field: "Courses",
                    formGroupTemplate: new FormGroupTemplateDescriptor
                    (
                        templateName: "FormGroupArrayTemplate"
                    ),
                    formsCollectionDisplayTemplate: new FormsCollectionDisplayTemplateDescriptor
                    (
                        templateName: "TextDetailTemplate",
                        bindings: new List<ItemBindingDescriptor>
                        {
                            new TextItemBindingDescriptor
                            (
                                name: "Text",
                                property: "DepartmentName",
                                stringFormat: "{0}",
                                textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                title: "",
                                fieldTypeSource: null
                            ),
                            new TextItemBindingDescriptor
                            (
                                name: "Detail",
                                property: "StartDate",
                                stringFormat: "{0:MMMM dd, yyyy}",
                                textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                title: "",
                                fieldTypeSource: null
                            )
                        }.ToDictionary(b => b.Name)
                    ),
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "CourseID",
                            type: "System.Int32",
                            title: "Course",
                            stringFormat: "{0}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "Credits",
                            type: "System.Int32",
                            title: "Credits",
                            stringFormat: "{0}",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select credits:",
                                loadingIndicatorText: "Loading ...",
                                textField: "Text",
                                valueField: "NumericValue",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new WhereDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                                filterBody: new EqualsBinaryDescriptor
                                                (
                                                    left: new MemberSelectorDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                        memberFullName: "ListName"
                                                    ),
                                                    right: new ConstantDescriptor
                                                    (
                                                        constantValue: "Credits",
                                                        type: typeof(string).AssemblyQualifiedName
                                                    )
                                                ),
                                                filterParameterName: "l"
                                            ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                memberFullName: "NumericValue"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            selectorParameterName: "l"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["NumericValue"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "NumericValue"
                                                ),
                                                ["Text"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "Text"
                                                )
                                            },
                                            newType: typeof(LookUpsModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "s"
                                    ),
                                    sourceElementType: typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(LookUpsModel).AssemblyQualifiedName!,
                                    dataType: typeof(LookUps).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<LookUps>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            placeholder: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "DepartmentID",
                            type: "System.Int32",
                            title: "Department",
                            stringFormat: "{0}",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select Department:",
                                loadingIndicatorText: "Loading ...",
                                textField: "Name",
                                valueField: "DepartmentID",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                memberFullName: "Name"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            selectorParameterName: "d"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["DepartmentID"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                    memberFullName: "DepartmentID"
                                                ),
                                                ["Name"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                    memberFullName: "Name"
                                                )
                                            },
                                            newType: typeof(DepartmentModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "d"
                                    ),
                                    sourceElementType: typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(DepartmentModel).AssemblyQualifiedName!,
                                    dataType: typeof(Department).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<Department>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            placeholder: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Title",
                            type: "System.String",
                            title: "Title",
                            stringFormat: "{0}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            placeholder: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    type: typeof(ICollection<CourseModel>).AssemblyQualifiedName!,
                    modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    keyFields: ["CourseID"],
                    title: "",
                    placeholder: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    validationMessages: [],
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            validationMessages: [],
            formType: FormType.Detail,
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );

        internal static DataFormSettingsDescriptor CourseForm = new
        (
            title: "Course",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Course/GetSingle",
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                modelType: "",
                dataType: "",
                filter: null,
                selectExpandDefinition: null
            ),
            fieldSettings:
            [
                new FormGroupBoxSettingsDescriptor
                (
                    groupHeader: "Name And ID",
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "CourseID",
                            type: "System.Int32",
                            title: "Course",
                            placeholder: "Course ID (required)",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Title",
                            type: "System.String",
                            title: "Title",
                            placeholder: "Title (required)",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    headerBindings: null,
                    isHidden: false
                ),
                new FormGroupBoxSettingsDescriptor
                (
                    groupHeader: "Title And Department",
                    fieldSettings:
                    [
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "DepartmentID",
                            type: "System.Int32",
                            title: "Department",
                            placeholder: "Department(required)",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select Department:",
                                loadingIndicatorText: "Loading ...",
                                textField: "Name",
                                valueField: "DepartmentID",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                memberFullName: "Name"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            selectorParameterName: "d"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["DepartmentID"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                    memberFullName: "DepartmentID"
                                                ),
                                                ["Name"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "d" ),
                                                    memberFullName: "Name"
                                                )
                                            },
                                            newType: typeof(DepartmentModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "d"
                                    ),
                                    sourceElementType: typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(DepartmentModel).AssemblyQualifiedName!,
                                    dataType: typeof(Department).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<Department>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            stringFormat: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "Credits",
                            type: "System.Int32",
                            title: "Credits",
                            placeholder: "Credits (required)",
                            dropDownTemplate: new DropDownTemplateDescriptor
                            (
                                templateName: "PickerTemplate",
                                titleText: "Select credits:",
                                loadingIndicatorText: "Loading ...",
                                textField: "Text",
                                valueField: "NumericValue",
                                textAndValueSelector: new SelectorLambdaDescriptor
                                (
                                    selector: new SelectDescriptor
                                    (
                                        sourceOperand: new OrderByDescriptor
                                        (
                                            sourceOperand: new WhereDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "$it" ),
                                                filterBody: new EqualsBinaryDescriptor
                                                (
                                                    left: new MemberSelectorDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                        memberFullName: "ListName"
                                                    ),
                                                    right: new ConstantDescriptor
                                                    (
                                                        constantValue: "Credits",
                                                        type: typeof(string).AssemblyQualifiedName
                                                    )
                                                ),
                                                filterParameterName: "l"
                                            ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor ( parameterName: "l" ),
                                                memberFullName: "NumericValue"
                                            ),
                                            sortDirection: LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            selectorParameterName: "l"
                                        ),
                                        selectorBody: new MemberInitDescriptor
                                        (
                                            memberBindings: new Dictionary<string, DescriptorBase>
                                            {
                                                ["NumericValue"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "NumericValue"
                                                ),
                                                ["Text"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor ( parameterName: "s" ),
                                                    memberFullName: "Text"
                                                )
                                            },
                                            newType: typeof(LookUpsModel).AssemblyQualifiedName
                                        ),
                                        selectorParameterName: "s"
                                    ),
                                    sourceElementType: typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName!,
                                    parameterName: "$it",
                                    bodyType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                ),
                                requestDetails: new RequestDetailsDescriptor
                                (
                                    dataSourceUrl: "api/Dropdown/GetObjectDropdown",
                                    modelType: typeof(LookUpsModel).AssemblyQualifiedName!,
                                    dataType: typeof(LookUps).AssemblyQualifiedName!,
                                    modelReturnType: typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName!,
                                    dataReturnType: typeof(IEnumerable<LookUps>).AssemblyQualifiedName!
                                ),
                                reloadItemsFlowName: null,
                                fieldTypeSource: null
                            ),
                            stringFormat: "",
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    headerBindings: null,
                    isHidden: false
                )
            ],
            validationMessages: [],
            formType: FormType.Detail,
            modelType: "",
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );
    }
}

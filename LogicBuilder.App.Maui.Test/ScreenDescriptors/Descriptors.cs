using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Directives;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;

namespace LogicBuilder.App.Maui.Test.ScreenDescriptors
{
    internal static class Descriptors
    {
        internal static DataFormSettingsDescriptor InstructorFormWithInlineOfficeAssignment = new
        (
            title: "Instructor",
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "FirstName",
                    rules:
                    [
                        new("RequiredRule", "First Name is required.")
                    ],
                    null
                ),
                new
                (
                    field: "LastName",
                    rules:
                    [
                        new("RequiredRule", "Last Name is required.")
                    ],
                    null
                ),
                new
                (
                    field: "HireDate",
                    rules:
                    [
                        new("RequiredRule", "Hire Date is required.")
                    ],
                    null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "ID",
                    title: "ID",
                    placeholder: "",
                    stringFormat: "",
                    type: "System.Int32",
                    textTemplate: new TextFieldTemplateDescriptor("HiddenTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0,
                        validators: null
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "FirstName",
                    title: "First Name",
                    placeholder: "First Name (required)",
                    stringFormat: "",
                    type: "System.String",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                "RequiredRule",
                                "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "LastName",
                    title: "Last Name",
                    placeholder: "Last Name (required)",
                    stringFormat: "",
                    type: "System.String",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                "RequiredRule",
                                "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "HireDate",
                    title: "Hire Date",
                    placeholder: "",
                    stringFormat: "",
                    type: "System.DateTime",
                    textTemplate: new TextFieldTemplateDescriptor("DateTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                "RequiredRule",
                                "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new FormGroupSettingsDescriptor
                (
                    field: "OfficeAssignment",
                    title: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    placeholder: "",
                    modelType: typeof(OfficeAssignmentModel).AssemblyQualifiedName!,
                    formGroupTemplate: new FormGroupTemplateDescriptor("InlineFormGroupTemplate"),
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Location",
                            title: "Location",
                            placeholder: "",
                            stringFormat: "",
                            type: "System.String",
                            textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
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
                    stringFormat: "",
                    placeholder: "Select Courses ...",
                    type: typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName!,
                    multiSelectTemplate: new MultiSelectTemplateDescriptor
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
                                    sourceOperand: new ParameterDescriptor("$it"),
                                    selectorBody: new MemberSelectorDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor("d"),
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
                                            "CourseID",
                                            new ParameterDescriptor("s")
                                        ),
                                        ["CourseTitle"] = new MemberSelectorDescriptor
                                        (
                                            "Title",
                                            new ParameterDescriptor("s")
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
                    validationSetting: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            requestDetails: new FormRequestDetailsDescriptor
            (
                "/Instructor/GetSingle",
                "",
                "",
                "",
                "",
                "",
                null,
                null
            ),
            formType: LogicBuilder.App.Maui.Forms.Configuration.DataForm.FormType.Add,
            headerBindings: null,
            subtitleBindings: null,
            conditionalDirectives: null,
            modelType: "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        );

        internal static DataFormSettingsDescriptor InstructorFormWithPopupOfficeAssignment = new
        (
            title: "Instructor",
            requestDetails: new FormRequestDetailsDescriptor
            (
                "/Instructor/GetSingle",
                "",
                "",
                "",
                "",
                "",
                null,
                null
            ),
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "FirstName",
                    rules:
                    [
                        new("RequiredRule", "First Name is required.")
                    ],
                    null
                ),
                new
                (
                    field: "LastName",
                    rules:
                    [
                        new("RequiredRule", "Last Name is required.")
                    ],
                    null
                ),
                new
                (
                    field: "HireDate",
                    rules:
                    [
                        new("RequiredRule", "Hire Date is required.")
                    ],
                    null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "ID",
                    title: "ID",
                    placeholder: "",
                    stringFormat: "",
                    type: "System.Int32",
                    textTemplate: new TextFieldTemplateDescriptor("HiddenTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0,
                        validators: null
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "FirstName",
                    title: "First Name",
                    placeholder: "First Name (required)",
                    stringFormat: "",
                    type: "System.String",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                "RequiredRule",
                                "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "LastName",
                    type: "System.String",
                    title: "Last Name",
                    placeholder: "Last Name (required)",
                    stringFormat: "",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "HireDate",
                    type: "System.DateTime",
                    title: "Hire Date",
                    placeholder: "",
                    stringFormat: "",
                    textTemplate: new TextFieldTemplateDescriptor("DateTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        validators:
                        [
                            new                            
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new FormGroupSettingsDescriptor
                (
                    field: "OfficeAssignment",
                    title: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    placeholder: "",
                    modelType: typeof(OfficeAssignmentModel).AssemblyQualifiedName!,
                    fieldSettings:
                    [
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Location",
                            type: "System.String",
                            title: "Location",
                            placeholder: "Location",
                            stringFormat: "",
                            textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                            updateOnlyTextTemplate: null,
                            validationSetting: null,
                            fieldTypeSource: null
                        )
                    ],
                    formGroupTemplate: new FormGroupTemplateDescriptor
                    (
                        templateName: "PopupFormGroupTemplate"
                    ),
                    validationMessages: [],
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null
                ),
                new MultiSelectFormControlSettingsDescriptor
                (
                    keyFields: ["CourseID"],
                    field: "Courses",
                    title:"Courses",
                    placeholder: "Select Courses ...",
                    stringFormat: "",
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
                                    sourceOperand: new ParameterDescriptor("$it"),
                                    selectorBody: new MemberSelectorDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor("d"),
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
                                            sourceOperand: new ParameterDescriptor("s"),
                                            memberFullName: "CourseID"
                                        ),
                                        ["CourseTitle"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor("s"),
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
                    validationSetting: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            formType: FormType.Add,
            headerBindings: null,
            subtitleBindings: null,
            conditionalDirectives: null
        );

        internal static DataFormSettingsDescriptor DepartmentForm = new
        (
            title: "Department",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Department/GetSingle",
                "",
                "",
                "",
                "",
                "",
                filter: null,
                selectExpandDefinition: null
            ),
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "Budget",
                    rules:
                    [
                        new ValidationRuleDescriptor (className: "RequiredRule", message: "Budget is required."),
                        new ValidationRuleDescriptor (className: "MustBePositiveNumberRule", message: "Budget must be a positive number.")
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "InstructorID",
                    rules:
                    [
                        new ValidationRuleDescriptor(className: "RequiredRule", message: "Administrator is required.")
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "Name",
                    rules:
                    [
                        new ValidationRuleDescriptor (className: "RequiredRule", message: "Name is required.")
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "StartDate",
                    rules:
                    [
                        new ValidationRuleDescriptor (className: "RequiredRule", message: "Start Date is required.")
                    ],
                    fieldTypeSource: null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "DepartmentID",
                    type: "System.Int32",
                    title: "ID",
                    placeholder: "",
                    stringFormat: "",
                    textTemplate: new TextFieldTemplateDescriptor("HiddenTemplate"),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0, []
                    ),
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Budget",
                    type: "System.Decimal",
                    title: "Budget",
                    placeholder: "Budget (required)",
                    stringFormat: "{0:F2}",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0m,
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                []
                            ),
                            new ValidatorDefinitionDescriptor
                            (
                                className: "MustBePositiveNumberRule",
                                functionName: "Check",
                                []
                            )
                        ]
                    ),
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                ),
                new DropdownSelectorControlSettingsDescriptor
                (
                    field: "InstructorID",
                    type: "System.Int32",
                    title: "Administrator",
                    placeholder: "Select Administrator (required)",
                    stringFormat: "",
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
                                    sourceOperand: new ParameterDescriptor("$it"),
                                    selectorBody: new MemberSelectorDescriptor
                                    (
                                        sourceOperand: new ParameterDescriptor("d"),
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
                                            sourceOperand: new ParameterDescriptor("s"),
                                            memberFullName: "ID"
                                        ),
                                        ["FirstName"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor("s"),
                                            memberFullName: "FirstName"
                                        ),
                                        ["LastName"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor("s"),
                                            memberFullName: "LastName"
                                        ),
                                        ["FullName"] = new MemberSelectorDescriptor
                                        (
                                            sourceOperand: new ParameterDescriptor("s"),
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
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0,
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Name",
                    type: "System.String",
                    title: "Name",
                    placeholder: "Name (required)",
                    stringFormat: "",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "StartDate",
                    type: "System.DateTime",
                    title: "Start Date",
                    placeholder: "",
                    stringFormat: "",
                    textTemplate: new TextFieldTemplateDescriptor("DateTemplate"),
                    updateOnlyTextTemplate: null,
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                null
                            )
                        ]
                    ),
                    fieldTypeSource: null
                ),
                new FormGroupArraySettingsDescriptor
                (
                    field: "Courses",
                    title: "Courses",
                    placeholder: "(Courses)",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    formGroupTemplate: new FormGroupTemplateDescriptor("FormGroupArrayTemplate"),
                    validationMessages: new List<ValidationMessageDescriptor>
                    {
                        new
                        (
                            field: "CourseID",
                            rules:
                            [
                                new (className: "RequiredRule", message: "CourseID is required."),
                                new (className: "MustBeIntegerRule", message: "CourseID must be an integer.")
                            ],
                            fieldTypeSource: null
                        ),
                        new
                        (
                            field: "Credits",
                            rules:
                            [
                                new (className: "RequiredRule", message: "Credits is required."),
                                new (className: "RangeRule", message: "Credits must be between 0 and 5 inclusive.")
                            ],
                            fieldTypeSource: null
                        ),
                        new
                        (
                            field: "DepartmentID",
                            rules:
                            [
                                new (className: "RequiredRule", message: "Department is required..")
                            ],
                            fieldTypeSource: null
                        ),
                        new
                        (
                            field: "Title",
                            rules:
                            [
                                new(className: "RequiredRule", message: "Title is required.")
                            ],
                            fieldTypeSource: null
                        )
                    }.ToDictionary(v => v.Field, v => v.Rules),
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
                                textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                                title: "",
                                fieldTypeSource: null
                            ),
                            new TextItemBindingDescriptor
                            (
                                name: "Detail",
                                property: "Credits",
                                stringFormat: "{0}",
                                textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
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
                            placeholder: "Course ID (required)",
                            textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        []
                                    ),
                                    new
                                    (
                                        className: "MustBeIntegerRule",
                                        functionName: "Check",
                                        []
                                    )
                                ]
                            ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
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
                                                sourceOperand: new ParameterDescriptor("$it"),
                                                filterBody: new EqualsBinaryDescriptor
                                                (
                                                    left: new MemberSelectorDescriptor
                                                    (
                                                        sourceOperand: new ParameterDescriptor("l"),
                                                        memberFullName: "ListName"
                                                    ),
                                                    right: new ConstantDescriptor
                                                    (
                                                        constantValue:"Credits",
                                                        type: typeof(string).AssemblyQualifiedName
                                                    )
                                                ),
                                                filterParameterName: "l"
                                            ),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor("l"),
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
                                                    sourceOperand: new ParameterDescriptor("s"),
                                                    memberFullName: "NumericValue"
                                                ),
                                                ["Text"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor("s"),
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        []
                                    ),
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RangeRule",
                                        functionName: "Check",
                                        arguments: new List<ValidatorArgumentDescriptor>
                                        {
                                            new ( name: "min", value: 0, type: "System.Int32" ),
                                            new ( name: "max", value: 5, type: "System.Int32" )
                                        }.ToDictionary(vad => vad.Name)
                                    )
                                ]
                            ),
                            stringFormat: "",
                            fieldTypeSource: null
                        ),
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
                                            sourceOperand: new ParameterDescriptor("$it"),
                                            selectorBody: new MemberSelectorDescriptor
                                            (
                                                sourceOperand: new ParameterDescriptor("d"),
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
                                                    sourceOperand: new ParameterDescriptor("d"),
                                                    memberFullName: "DepartmentID"
                                                ),
                                                ["Name"] = new MemberSelectorDescriptor
                                                (
                                                    sourceOperand: new ParameterDescriptor("d"),
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className:  "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Title",
                            type: "System.String",
                            title: "Title",
                            placeholder: "Title (required)",
                            textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: "",
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className:  "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        )
                    ],
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null,
                    type: typeof(ICollection<CourseModel>).AssemblyQualifiedName!,
                    modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    keyFields: ["CourseID"]
                )
            ],
            modelType: "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            formType: FormType.Add,
            headerBindings: null,
            subtitleBindings: null,
            conditionalDirectives: null
        );

        internal static DataFormSettingsDescriptor DepartmentFormWithAllItemsGrouped = new
        (
            title: "Department",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Department/GetSingle",
                "",
                "",
                "",
                "",
                "",
                filter: null,
                selectExpandDefinition: null
            ),
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "Budget",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Budget is required." ),
                        new ValidationRuleDescriptor ( className: "MustBePositiveNumberRule", message: "Budget must be a positive number." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "InstructorID",
                    rules:
                    [
                        new ValidationRuleDescriptor(className: "RequiredRule", message: "Administrator is required." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "Name",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Name is required." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "StartDate",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Start Date is required." )
                    ],
                    fieldTypeSource: null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators: null
                            ),
                            placeholder: "",
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Budget",
                            type: "System.Decimal",
                            title: "Budget",
                            placeholder: "Budget (required)",
                            stringFormat: "{0:F2}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0m,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    ),
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "MustBePositiveNumberRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "InstructorID",
                            type: "System.Int32",
                            title: "Administrator",
                            placeholder: "Select Administrator (required)",
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
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
                            placeholder: "Name (required)",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: "",
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "StartDate",
                            type: "System.DateTime",
                            title: "Start Date",
                            placeholder: "",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "DateTemplate" ),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        ),
                        new FormGroupArraySettingsDescriptor
                        (
                            field: "Courses",
                            placeholder: "(Courses)",
                            formGroupTemplate: new FormGroupTemplateDescriptor
                            (
                                templateName: "FormGroupArrayTemplate"
                            ),
                            validationMessages: new List<ValidationMessageDescriptor>
                            {
                                new
                                (
                                    field: "CourseID",
                                    rules:
                                    [
                                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "CourseID is required." ),
                                        new ValidationRuleDescriptor ( className: "MustBeIntegerRule", message: "CourseID must be an integer." )
                                    ],
                                    fieldTypeSource: null
                                ),
                                new
                                (
                                    field: "Credits",
                                    rules:
                                    [
                                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Credits is required." ),
                                        new ValidationRuleDescriptor ( className: "RangeRule", message: "Credits must be between 0 and 5 inclusive." )
                                    ],
                                    fieldTypeSource: null
                                ),
                                new
                                (
                                    field: "DepartmentID",
                                    rules:
                                    [
                                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Department is required.." )
                                    ],
                                    fieldTypeSource: null
                                ),
                                new
                                (
                                    field: "Title",
                                    rules:
                                    [
                                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Title is required." )
                                    ],
                                    fieldTypeSource: null
                                )
                            }.ToDictionary(v => v.Field, v => v.Rules),
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
                                    placeholder: "Course ID (required)",
                                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                    validationSetting: new FieldValidationSettingsDescriptor
                                    (
                                        defaultValue: 0,
                                        validators:
                                        [
                                            new ValidatorDefinitionDescriptor
                                            (
                                                className: "RequiredRule",
                                                functionName: "Check",
                                                arguments: null
                                            ),
                                            new ValidatorDefinitionDescriptor
                                            (
                                                className: "MustBeIntegerRule",
                                                functionName: "Check",
                                                arguments: null
                                            )
                                        ]
                                    ),
                                    stringFormat: "",
                                    updateOnlyTextTemplate: null,
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
                                    validationSetting: new FieldValidationSettingsDescriptor
                                    (
                                        defaultValue: 0,
                                        validators:
                                        [
                                            new ValidatorDefinitionDescriptor
                                            (
                                                className: "RequiredRule",
                                                functionName: "Check",
                                                arguments: null
                                            ),
                                            new ValidatorDefinitionDescriptor
                                            (
                                                className: "RangeRule",
                                                functionName: "Check",
                                                arguments: new List<ValidatorArgumentDescriptor>
                                                {
                                                    new ( name: "min", value: 0, type: "System.Int32" ),
                                                    new ( name: "max", value: 5, type: "System.Int32" )
                                                }.ToDictionary(vad => vad.Name)
                                            )
                                        ]
                                    ),
                                    stringFormat: "",
                                    fieldTypeSource: null
                                ),
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
                                    validationSetting: new FieldValidationSettingsDescriptor
                                    (
                                        defaultValue: 0,
                                        validators:
                                        [
                                            new ValidatorDefinitionDescriptor
                                            (
                                                className: "RequiredRule",
                                                functionName: "Check",
                                                arguments: null
                                            )
                                        ]
                                    ),
                                    stringFormat: "",
                                    fieldTypeSource: null
                                ),
                                new InputFieldControlSettingsDescriptor
                                (
                                    field: "Title",
                                    type: "System.String",
                                    title: "Title",
                                    placeholder: "Title (required)",
                                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                                    validationSetting: new FieldValidationSettingsDescriptor
                                    (
                                        defaultValue: "",
                                        validators:
                                        [
                                            new ValidatorDefinitionDescriptor
                                            (
                                                className: "RequiredRule",
                                                functionName: "Check",
                                                arguments: null
                                            )
                                        ]
                                    ),
                                    stringFormat: "",
                                    updateOnlyTextTemplate: null,
                                    fieldTypeSource: null
                                )
                            ],
                            type: typeof(ICollection<CourseModel>).AssemblyQualifiedName!,
                            modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            keyFields: ["CourseID"],
                            title: "",
                            validFormControlText: "",
                            invalidFormControlText: "",
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
            formType: FormType.Add,
            headerBindings: null,
            subtitleBindings: null,
            conditionalDirectives: null
        );

        internal static DataFormSettingsDescriptor DepartmentFormWithSomesItemsGrouped = new
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
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "Budget",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Budget is required." ),
                        new ValidationRuleDescriptor ( className: "MustBePositiveNumberRule", message: "Budget must be a positive number." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "InstructorID",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Administrator is required." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "Name",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Name is required." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "StartDate",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Start Date is required." )
                    ],
                    fieldTypeSource: null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators: null
                            ),
                            placeholder: "",
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Budget",
                            type: "System.Decimal",
                            title: "Budget",
                            placeholder: "Budget (required)",
                            stringFormat: "{0:F2}",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0m,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    ),
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "MustBePositiveNumberRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        ),
                        new DropdownSelectorControlSettingsDescriptor
                        (
                            field: "InstructorID",
                            type: "System.Int32",
                            title: "Administrator",
                            placeholder: "Select Administrator (required)",
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            fieldTypeSource: null
                        )
                    ],
                    headerBindings: null,
                    isHidden: false
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Name",
                    type: "System.String",
                    title: "Name",
                    placeholder: "Name (required)",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "StartDate",
                    type: "System.DateTime",
                    title: "Start Date",
                    placeholder: "",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "DateTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                ),
                new FormGroupArraySettingsDescriptor
                (
                    field: "Courses",
                    placeholder: "(Courses)",
                    formGroupTemplate: new FormGroupTemplateDescriptor
                    (
                        templateName: "FormGroupArrayTemplate"
                    ),
                    validationMessages: new List<ValidationMessageDescriptor>
                    {
                        new
                        (
                            field: "CourseID",
                            rules:
                            [
                                new ValidationRuleDescriptor ( className: "RequiredRule", message: "CourseID is required." ),
                                new ValidationRuleDescriptor ( className: "MustBeIntegerRule", message: "CourseID must be an integer." )
                            ],
                            fieldTypeSource: null
                        ),
                        new
                        (
                            field: "Credits",
                            rules:
                            [
                                new ValidationRuleDescriptor ( className: "RequiredRule", message: "Credits is required." ),
                                new ValidationRuleDescriptor ( className: "RangeRule", message: "Credits must be between 0 and 5 inclusive." )
                            ],
                            fieldTypeSource: null
                        ),
                        new
                        (
                            field: "DepartmentID",
                            rules:
                            [
                                new ValidationRuleDescriptor ( className: "RequiredRule", message: "Department is required.." )
                            ],
                            fieldTypeSource: null
                        ),
                        new
                        (
                            field: "Title",
                            rules:
                            [
                                new ValidationRuleDescriptor ( className: "RequiredRule", message: "Title is required." )
                            ],
                            fieldTypeSource: null
                        )
                    }.ToDictionary(v => v.Field, v => v.Rules),
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
                            placeholder: "Course ID (required)",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    ),
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "MustBeIntegerRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    ),
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RangeRule",
                                        functionName: "Check",
                                        arguments: new List<ValidatorArgumentDescriptor>
                                        {
                                            new ( name: "min", value: 0, type: "System.Int32" ),
                                            new ( name: "max", value: 5, type: "System.Int32" )
                                        }.ToDictionary(vad => vad.Name)
                                    )
                                ]
                            ),
                            stringFormat: "",
                            fieldTypeSource: null
                        ),
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
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: 0,
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            fieldTypeSource: null
                        ),
                        new InputFieldControlSettingsDescriptor
                        (
                            field: "Title",
                            type: "System.String",
                            title: "Title",
                            placeholder: "Title (required)",
                            textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                            validationSetting: new FieldValidationSettingsDescriptor
                            (
                                defaultValue: "",
                                validators:
                                [
                                    new ValidatorDefinitionDescriptor
                                    (
                                        className: "RequiredRule",
                                        functionName: "Check",
                                        arguments: null
                                    )
                                ]
                            ),
                            stringFormat: "",
                            updateOnlyTextTemplate: null,
                            fieldTypeSource: null
                        )
                    ],
                    type: typeof(ICollection<CourseModel>).AssemblyQualifiedName!,
                    modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    keyFields: ["CourseID"],
                    title: "",
                    validFormControlText: "",
                    invalidFormControlText: "",
                    conditionalDirectives: null,
                    headerBindings: null,
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            formType: FormType.Add,
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
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "CourseID",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "CourseID is required." ),
                        new ValidationRuleDescriptor ( className: "MustBeIntegerRule", message: "CourseID must be an integer." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "Credits",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Credits is required." ),
                        new ValidationRuleDescriptor ( className: "RangeRule", message: "Credits must be between 0 and 5 inclusive." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "DepartmentID",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Department is required.." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "Title",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Title is required." )
                    ],
                    fieldTypeSource: null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "CourseID",
                    type: "System.Int32",
                    title: "Course",
                    placeholder: "Course ID (required)",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    updateOnlyTextTemplate: new TextFieldTemplateDescriptor ( templateName: "LabelTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0,
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            ),
                            new ValidatorDefinitionDescriptor
                            (
                                className: "MustBeIntegerRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
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
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0,
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            ),
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RangeRule",
                                functionName: "Check",
                                arguments: new List<ValidatorArgumentDescriptor>
                                {
                                    new ( name: "min", value: 0, type: "System.Int32" ),
                                    new ( name: "max", value: 5, type: "System.Int32" )
                                }.ToDictionary(v => v.Name)
                            )
                        ]
                    ),
                    stringFormat: "",
                    fieldTypeSource: null
                ),
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
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: 0,
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "Title",
                    type: "System.String",
                    title: "Title",
                    placeholder: "Title (required)",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                )
            ],
            modelType: "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            formType: FormType.Add,
            headerBindings: null,
            conditionalDirectives: null,
            subtitleBindings: null
        );

        internal static DataFormSettingsDescriptor StudentForm = new
        (
            title: "Student",
            requestDetails: new FormRequestDetailsDescriptor
            (
                getUrl: "/Student/GetSingle",
                addUrl: "",
                updateUrl: "",
                deleteUrl: "",
                modelType: "",
                dataType: "",
                filter: null,
                selectExpandDefinition: null
            ),
            validationMessages: new List<ValidationMessageDescriptor>
            {
                new
                (
                    field: "FirstName",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "First Name is required." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "LastName",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Last Name is required." )
                    ],
                    fieldTypeSource: null
                ),
                new
                (
                    field: "EnrollmentDate",
                    rules:
                    [
                        new ValidationRuleDescriptor ( className: "RequiredRule", message: "Enrollment Date is required." )
                    ],
                    fieldTypeSource: null
                )
            }.ToDictionary(v => v.Field, v => v.Rules),
            fieldSettings:
            [
                new InputFieldControlSettingsDescriptor
                (
                    field: "FirstName",
                    type: "System.String",
                    title: "First Name",
                    placeholder: "First Name (required)",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "LastName",
                    type: "System.String",
                    title: "Last Name",
                    placeholder: "Last Name (required)",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "TextTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: "",
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                ),
                new InputFieldControlSettingsDescriptor
                (
                    field: "EnrollmentDate",
                    type: "System.DateTime",
                    title: "Enrollment Date",
                    placeholder: "",
                    textTemplate: new TextFieldTemplateDescriptor ( templateName: "DateTemplate" ),
                    validationSetting: new FieldValidationSettingsDescriptor
                    (
                        defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                        validators:
                        [
                            new ValidatorDefinitionDescriptor
                            (
                                className: "RequiredRule",
                                functionName: "Check",
                                arguments: null
                            )
                        ]
                    ),
                    stringFormat: "",
                    updateOnlyTextTemplate: null,
                    fieldTypeSource: null
                )
            ],
            conditionalDirectives: new List<VariableDirectivesDescriptor>
            {
                new
                (
                    field: "EnrollmentDate",
                    conditionalDirectives:
                    [
                        new DirectiveDescriptor
                        (
                            definition: new DirectiveDefinitionDescriptor
                            (
                                className: "ValidateIf",
                                functionName: "Check",
                                arguments: null
                            ),
                            condition: new FilterLambdaDescriptor
                            (
                                sourceElementType: typeof(StudentModel).AssemblyQualifiedName!,
                                parameterName: "f",
                                filterBody: new EqualsBinaryDescriptor
                                (
                                    left: new MemberSelectorDescriptor
                                    (
                                        memberFullName: "FirstName",
                                        sourceOperand: new ParameterDescriptor( parameterName: "f" )
                                    ),
                                    right: new MemberSelectorDescriptor
                                    (
                                        memberFullName: "LastName",
                                        sourceOperand: new ParameterDescriptor( parameterName: "f" )
                                    )
                                )
                            )
                        )
                    ],
                    fieldTypeSource: null
                )
            }.ToDictionary(vd => vd.Field, vd => vd.ConditionalDirectives),
            modelType: "Contoso.Domain.Entities.StudentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
            formType: FormType.Add,
            headerBindings: null,
            subtitleBindings: null
        );

        internal static IList<CommandButtonDescriptor> ButtonDescriptors =
        [
            new CommandButtonDescriptor ( id: 1, longString: "Save", shortString: "S", command: "SubmitCommand", buttonIcon: "Save" ),
            new CommandButtonDescriptor ( id: 2, longString: "Home", shortString: "H", command: "NavigateCommand", buttonIcon: "Home" )
        ];

        internal static NavigationBarDescriptor GetNavigationBar(string currentModule) => new
        (
            brandText: "Contoso",
            currentModule: currentModule,
            menuItems:
            [
                new NavigationMenuItemDescriptor
                (
                    initialModule: "home",
                    icon: "Home",
                    text: "Home",
                    active: true,
                    subItems: []
                ),
                new NavigationMenuItemDescriptor
                (
                    initialModule: "about",
                    icon: "University",
                    text: "About",
                    active: false,
                    subItems: []
                ),
                new NavigationMenuItemDescriptor
                (
                    initialModule: "students",
                    icon: "Users",
                    text: "Students",
                    active: false,
                    subItems: []
                ),
                new NavigationMenuItemDescriptor
                (
                    initialModule: "courses",
                    icon: "BookOpen",
                    text: "Courses",
                    active: false,
                    subItems: []
                ),
                new NavigationMenuItemDescriptor
                (
                    initialModule: "departments",
                    icon: "Building",
                    text: "Departments",
                    active: false,
                    subItems: []
                ),
                new NavigationMenuItemDescriptor
                (
                    initialModule: "instructors",
                    icon: "ChalkboardTeacher",
                    text: "Instructors",
                    active: false,
                    subItems: []
                )
            ]
        );
    }
}

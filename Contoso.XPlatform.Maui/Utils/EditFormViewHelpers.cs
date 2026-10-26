using Contoso.XPlatform.Behaviours;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Converters;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;

namespace Contoso.XPlatform.Utils
{
    internal static class EditFormViewHelpers
    {
        public static MultiSelectItemTemplateSelector GetMultiSelectItemTemplateSelector(MultiSelectTemplateDescriptor multiSelectTemplateDescriptor)
        {
            return new MultiSelectItemTemplateSelector
            {
                SingleFieldTemplate = new DataTemplate
                (
                    () => new Grid
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectItemStyle),
                        Children =
                        {
                            new VerticalStackLayout
                            {
                                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectSingleFieldLayoutStyle),
                                Children =
                                {
                                    new Label
                                    {
                                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectSingleFieldLabelStyle),
                                    }.AddBinding(Label.TextProperty, new Binding(multiSelectTemplateDescriptor.TextField))
                                }
                            }
                        }
                    }
                )
            };
        }

        public static QuestionTemplateSelector QuestionTemplateSelector { get; } = new QuestionTemplateSelector
        {
            TextTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetEntryForValidation(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            LabelTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetLabelControl(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            PasswordTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPasswordEntryForValidation(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            DateTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                       GetDatePickerForValidation(),
                       GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            CheckboxTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetCheckboxForValidation(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            SwitchTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetSwitchForValidation(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            PickerTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPickerForValidation(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            MultiSelectTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetMultiSelectFieldControl(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            PopupFormGroupTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPopupFormFieldControl(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            FormGroupArrayTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPopupFormArrayFieldControl(),
                        GetLabelForValidation()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            HiddenTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HiddenTemplateStyle)
                }
            )
        };

        public static HorizontalStackLayout GetCheckboxForValidation()
            => new()
            {
                Children =
                {
                    new CheckBox
                    {
                        Behaviors =
                        {
                            new EventToCommandBehavior()
                            {
                                EventName = nameof(CheckBox.CheckedChanged)
                            }
                            .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(CheckboxValidatableObject.CheckedChangedCommand)))
                        }
                    }
                    .AddBinding(CheckBox.IsCheckedProperty, new Binding(nameof(CheckboxValidatableObject.Value))),
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.CheckBoxLabelStyle)
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(CheckboxValidatableObject.CheckboxLabel)))
                    .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
                }
            };

        public static HorizontalStackLayout GetSwitchForValidation()
            => new()
            {
                Children =
                {
                    new Switch
                    {
                        Behaviors =
                        {
                            new EventToCommandBehavior()
                            {
                                EventName = nameof(Switch.Toggled)
                            }
                            .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(SwitchValidatableObject.ToggledCommand)))
                        }
                    }
                    .AddBinding(Switch.IsToggledProperty, new Binding(nameof(SwitchValidatableObject.Value))),
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SwitchLabelStyle)
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(SwitchValidatableObject.SwitchLabel)))
                    .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
                }
            };

        public static Picker GetPickerForValidation()
        {
            Picker picker = new Picker()
            {
                Behaviors =
                {
                    new PickerValidationBehavior()
                        .AddBinding(PickerValidationBehavior.IsValidProperty, new Binding(nameof(PickerValidatableObject<>.IsValid)))
                        .AddBinding(PickerValidationBehavior.IsDirtyProperty, new Binding(nameof(PickerValidatableObject<>.IsDirty))),
                    new EventToCommandBehavior()
                    {
                        EventName = nameof(Picker.SelectedIndexChanged)
                    }
                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(PickerValidatableObject<>.SelectedIndexChangedCommand)))
                }
            }
            .AddBinding(Picker.SelectedItemProperty, new Binding(nameof(PickerValidatableObject<>.SelectedItem), BindingMode.TwoWay))
            .AddBinding(Picker.TitleProperty, new Binding(nameof(PickerValidatableObject<>.Title)))
            .AddBinding(Picker.ItemsSourceProperty, new Binding(nameof(PickerValidatableObject<>.Items)))
            .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)));

            picker.ItemDisplayBinding = new Binding
            (
                path: ".",
                converter: new PickerItemDisplayPathConverter(),
                converterParameter: picker
            );

            return picker;
        }

        public static Grid GetMultiSelectFieldControl()
        {
            return new()
            {
                Children =
                {
                    GetEntryForMultiSelectControl(),
                    new BoxView
                    {
                        GestureRecognizers =
                        {
                            new TapGestureRecognizer().AddBinding
                            (
                                TapGestureRecognizer.CommandProperty,
                                new Binding(path: nameof(MultiSelectValidatableObject<,>.OpenCommand))
                            )
                        }
                    }
                }
            };
        }

        public static Grid GetPopupFormFieldControl()
        {
            return new Grid
            {
                Children =
                {
                    GetEntryForFormPopupControl(),
                    new BoxView
                    {
                        GestureRecognizers =
                        {
                            new TapGestureRecognizer().AddBinding
                            (
                                TapGestureRecognizer.CommandProperty,
                                new Binding(path: nameof(FormValidatableObject<>.OpenCommand))
                            )
                        }
                    }
                }
            };
        }

        public static Grid GetPopupFormArrayFieldControl()
        {
            return new Grid
            {
                Children =
                {
                    GetEntryForFormArrayPopupControl(),
                    new BoxView
                    {
                        GestureRecognizers =
                        {
                            new TapGestureRecognizer().AddBinding
                            (
                                TapGestureRecognizer.CommandProperty,
                                new Binding(path: nameof(FormArrayValidatableObject<,>.OpenCommand))
                            )
                        }
                    }
                }
            };
        }

        public static DatePicker GetDatePickerForValidation()
            => new DatePicker()
            {
                Behaviors =
                {
                    new EventToCommandBehavior()
                    {
                        EventName = nameof(DatePicker.DateSelected)
                    }
                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(DatePickerValidatableObject<>.DateChangedCommand)))
                }
            }
            .AddBinding(DatePicker.DateProperty, new Binding(nameof(DatePickerValidatableObject<>.Value)))
            .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)));

        public static Entry GetEntryForMultiSelectControl()
            => GetEntry().AddBinding
            (
                Entry.TextProperty,
                new Binding(nameof(MultiSelectValidatableObject<,>.DisplayText))
            );

        public static Entry GetEntryForFormPopupControl()
            => GetEntry().AddBinding
            (
                Entry.TextProperty,
                new Binding(nameof(FormValidatableObject<>.DisplayText))
            );

        public static Entry GetEntryForFormArrayPopupControl()
            => GetEntry().AddBinding
            (
                Entry.TextProperty,
                new Binding(nameof(FormArrayValidatableObject<,>.DisplayText))
            );

        public static Entry GetEntryForValidation(bool isPassword = false)
        {
            return AddBindingWithStringFormat(GetEntry(isPassword));

            static Entry AddBindingWithStringFormat(Entry entry)
                => entry.AddBinding
                (
                    Entry.TextProperty,
                    new Binding
                    (
                        path: nameof(EntryValidatableObject<>.Value),
                        converter: new StringFormatConverter(),
                        converterParameter: entry
                    )
                );
        }

        public static Entry GetPasswordEntryForValidation()
            => GetEntryForValidation(isPassword: true);

        public static Entry GetEntry(bool isPassword = false)
            => new Entry()
            {
                IsPassword = isPassword,
                Behaviors =
                    {
                        new EntryLineValidationBehavior()
                            .AddBinding(EntryLineValidationBehavior.IsValidProperty, new Binding(nameof(EntryValidatableObject<>.IsValid)))
                            .AddBinding(EntryLineValidationBehavior.IsDirtyProperty, new Binding(nameof(EntryValidatableObject<>.IsDirty))),
                        new EventToCommandBehavior()
                        {
                            EventName = nameof(Entry.TextChanged)
                        }
                        .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(EntryValidatableObject<>.TextChangedCommand)))
                    }
            }
            .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(EntryValidatableObject<>.Placeholder)))
            .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)));

        public static Label GetLabelForValidation()
            => new Label
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ValidationMessageLabelStyle),
                Behaviors =
                {
                    new ErrorLabelValidationBehavior()
                        .AddBinding(ErrorLabelValidationBehavior.IsValidProperty, new Binding(nameof(EntryValidatableObject<>.IsValid)))
                        .AddBinding(ErrorLabelValidationBehavior.IsDirtyProperty, new Binding(nameof(EntryValidatableObject<>.IsDirty)))
                }
            }
            .AddBinding(Label.TextProperty, new Binding(path: nameof(ValidatableObjectBase<>.Errors), converter: new FirstValidationErrorConverter()));

        public static string GetFontAwesomeFontFamily()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
                return FontAwesomeFontFamily.AndroidSolid;
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
                return FontAwesomeFontFamily.iOSSolid;
            else if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                return FontAwesomeFontFamily.MacCatalystSolid;
            else if (DeviceInfo.Platform == DevicePlatform.Tizen)
                return FontAwesomeFontFamily.TizenSolid;
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
                return FontAwesomeFontFamily.WinUISolid;
            else
                throw new InvalidOperationException($"Invalid paltform. {DeviceInfo.Platform}");
        }

        private static Label GetLabelControl()
            => GetLabel
            (
                nameof(LabelValidatableObject<>.Title),
                nameof(LabelValidatableObject<>.DisplayText)
            )
            .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(LabelValidatableObject<>.Placeholder)));

        private static Label GetLabel(string titleBinding, string valueBinding, bool isPassword = false)
            => new ()
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.EditFormLabel),
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span{ FontAttributes = FontAttributes.Italic }.AddBinding(Span.TextProperty, new Binding(titleBinding)),
                        new Span { Text = ":  " },
                        isPassword
                            ? new Span{ FontAttributes = FontAttributes.Bold, Text = "*****" }
                            : new Span{ FontAttributes = FontAttributes.Bold }.AddBinding(Span.TextProperty, new Binding(valueBinding))
                    }
                }
            };
    }
}

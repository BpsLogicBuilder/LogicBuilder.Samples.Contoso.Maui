using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Converters;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Contoso.XPlatform.Views
{
    public partial class EditFormViewCS : ContentPage
    {
        public EditFormViewCS(EditFormViewModelBase editFormViewModel, IConverterFactory converterFactory)
        {
            this.editFormEntityViewModel = editFormViewModel;
            this.converterFactory = converterFactory;
            AddContent();
            BindingContext = this.editFormEntityViewModel;
        }

        private readonly EditFormViewModelBase editFormEntityViewModel;
        private readonly IConverterFactory converterFactory;
        private Grid transitionGrid;
        private Grid page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        [MemberNotNull(nameof(transitionGrid), nameof(page))]
        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.editFormEntityViewModel.Buttons);
            Title = editFormEntityViewModel.FormSettings.Title;

            static BindingBase GetHeaderBinding(MultiBindingDescriptor? multiBindingDescriptor, string bindingName)
            {
                if (multiBindingDescriptor == null)
                    return new Binding(bindingName);

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = [.. multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(ControlGroupBox.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IValidatable.Value)}")
                    )
                    .Cast<BindingBase>()]
                };
            }

            page = new Grid
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.EditFormGridStyle),
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                Children =
                {
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                    }
                    .AddBinding
                    (
                        Label.TextProperty,
                        GetHeaderBinding
                        (
                            editFormEntityViewModel.FormSettings.HeaderBindings,
                            $"{nameof(EditFormViewModelBase.FormSettings)}.{nameof(DataFormSettingsDescriptor.Title)}"
                        )
                    )
                    .SetGridRow(0),
                    new ScrollView
                    {
                        Content = editFormEntityViewModel.FormLayout.ControlGroupBoxList.Aggregate
                        (
                            new VerticalStackLayout(),
                            (stackLayout, controlBox) =>
                            {
                                if (!controlBox.IsVisible)
                                    return stackLayout;

                                stackLayout.Children.Add
                                (
                                    new Label
                                    {
                                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.EditFormGroupHeaderStyle),
                                        BindingContext = controlBox
                                    }
                                    .AddBinding
                                    (
                                        Label.TextProperty,
                                        GetHeaderBinding(controlBox.HeaderBindings, $"{nameof(ControlGroupBox.GroupHeader)}")
                                    )
                                );
                                stackLayout.Children.Add
                                (
                                    new VerticalStackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Start,
                                        BindingContext = controlBox
                                    }
                                    .AddBinding(BindableLayout.ItemsSourceProperty, new Binding("."))
                                    .SetDataTemplateSelector(EditFormViewHelpers.GetQuestionTemplateSelector(converterFactory))
                                );

                                return stackLayout;
                            }
                        )
                    }
                    .SetGridRow(1)
                }
            };

            transitionGrid = new Grid
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TransitionGridStyle)
            };

            Content = new Grid
            {
                Children =
                {
                    page,
                    transitionGrid
                }
            };
        }
    }
}
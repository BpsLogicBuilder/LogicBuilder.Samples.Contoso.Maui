using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace LogicBuilder.App.Maui.Views
{
    public partial class MultiSelectPageCS : ContentPage
    {
        public MultiSelectPageCS(IValidatable multiSelectValidatable)
        {
            this.multiSelectValidatable = multiSelectValidatable;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)
            (
                (
                    this.multiSelectValidatable.GetType()
                    .GetProperty(nameof(MultiSelectValidatableObject<,>.MultiSelectTemplate)) 
                        ?? throw new ArgumentException($"{nameof(MultiSelectValidatableObject<,>.MultiSelectTemplate)}: {{6841CE95-75AE-4F3D-A541-3D807E5039AB}}")
                )
                .GetValue(this.multiSelectValidatable) ?? throw new ArgumentException($"{nameof(multiSelectValidatable)}: {{39C66580-AC69-4030-BCC6-9C81BAC2F1F3}}")
            );

            Content = new AbsoluteLayout
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogAbsoluteLayoutStyle),
                Children =
                {
                    new ContentView
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogContentViewStyle),
                        Content = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupViewStyle),
                            RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                            },
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupHeaderStyle),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupHeaderLabelStyle),
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(MultiSelectValidatableObject<,>.Title)))
                                    }
                                }
                                .SetGridRow(0),
                                new ScrollView
                                {
                                    Content = new Grid
                                    {
                                        Children =
                                        {
                                            new CollectionView
                                            {
                                                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupCollectionViewStyle),
                                                ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                            }
                                            .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectValidatableObject<,>.Items)))
                                            /*SelectedItems not being bound on windows https://github.com/dotnet/maui/issues/8435 */
                                            .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectValidatableObject<,>.SelectedItems)))
                                        }
                                    }
                                }
                                .SetGridRow(1),
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterSeparatorStyle) }
                                .SetGridRow(2),
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterStyle),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupCancelButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectValidatableObject<,>.CancelCommand)))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupAcceptButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectValidatableObject<,>.SubmitCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                                .SetGridRow(3),
                            }
                        }
                    }
                }
            };

            this.BackgroundColor = Colors.Transparent;
            this.BindingContext = this.multiSelectValidatable;
        }

        private readonly IValidatable multiSelectValidatable;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}
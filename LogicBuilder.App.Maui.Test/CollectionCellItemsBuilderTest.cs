using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Test.Helpers;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.ViewModels.ReadOnlys;

namespace LogicBuilder.App.Maui.Test
{
    public class CollectionCellItemsBuilderTest
    {
        public CollectionCellItemsBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields
        [Fact]
        public void CreateReadOnlyPropertiesForInstructorModel()
        {
            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                [
                    new TextItemBindingDescriptor
                    (
                        name: "Text",
                        property: "HireDate",
                        stringFormat: "{0:MMMM dd, yyyy}",
                        textTemplate: new TextFieldTemplateDescriptor("DateTemplate"),
                        fieldTypeSource: null,
                        title: ""
                    ),
                    new TextItemBindingDescriptor
                    (
                        name: "Detail",
                        property: "FullName",
                        stringFormat: "{0}",
                        textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                        fieldTypeSource: null,
                        title: ""
                    )
                ],
                typeof(InstructorModel)
            ).CreateFields();

            Assert.Equal(2, properties.Count);
        }

        [Fact]
        public void CreateReadOnlyPropertiesForInstructorModelWithNavigationPrpertu()
        {
            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                [
                    new TextItemBindingDescriptor
                    (
                        name: "Text",
                        property: "HireDate",
                        stringFormat: "{0:MMMM dd, yyyy}",
                        textTemplate: new TextFieldTemplateDescriptor("DateTemplate"),
                        fieldTypeSource: null,
                        title: ""
                    ),
                    new TextItemBindingDescriptor
                    (
                        name: "Detail",
                        property: "FullName",
                        stringFormat: "{0}",
                        textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                        fieldTypeSource: null,
                        title: ""
                    ),
                    new TextItemBindingDescriptor
                    (
                        name: "Detail",
                        property: "OfficeAssignment.Location",
                        stringFormat: "{0}",
                        textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                        fieldTypeSource: null,
                        title: ""
                    )
                ],
                typeof(InstructorModel)
            )
            .CreateFields();

            Assert.Equal(3, properties.Count);
        }

        private ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(List<ItemBindingDescriptor> bindingDescriptors, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetCollectionCellItemsBuilder
            (
                modelType,
                bindingDescriptors
            );
        }
    }
}

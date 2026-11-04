using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Test.Helpers;
using LogicBuilder.App.Maui.Test.ScreenDescriptors;
using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.ViewModels.Factories;

namespace LogicBuilder.App.Maui.Test
{
    public class ReadOnlyFieldsCollectionBuilderTest
    {
        public ReadOnlyFieldsCollectionBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CreateDetailFormLayoutForDepartment_NoGroups()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields();

            var item = Assert.Single(formLayout.ControlGroupBoxList);
            Assert.Equal(6, item.Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.Single().GroupHeader);
        }

        [Fact]
        public void CreateDetailFormLayoutForDepartment_AllFieldsGrouped()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentFormWithAllItemsGrouped,
                typeof(DepartmentModel)
            )
            .CreateFields();

            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupTwo").Count);
        }

        [Fact]
        public void CreateDetailFormLayoutForDepartment_SomeFieldsGrouped()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentFormWithSomeItemsGrouped,
                typeof(DepartmentModel)
            )
            .CreateFields();


            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Department").Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList[0].GroupHeader);
        }

        private IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(DataFormSettingsDescriptor dataFormSettingsDescriptor, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetReadOnlyFieldsCollectionBuilder
            (
                modelType,
                dataFormSettingsDescriptor.FieldSettings,
                dataFormSettingsDescriptor,
                null,
                null
            );
        }
    }
}

using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Test.Helpers;
using LogicBuilder.App.Maui.Test.ScreenDescriptors;
using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace LogicBuilder.App.Maui.Test
{
    public class FieldsCollectionBuilderTest
    {
        public FieldsCollectionBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapCourseModelToIValidatableList()
        {
            //act
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.CourseForm,
                typeof(CourseModel)
            )
            .CreateFields()
            .Properties;

            //assert
            Dictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            Assert.Equal(typeof(EntryValidatableObject<int>), propertiesDictionary["CourseID"].GetType());
            Assert.Equal(typeof(EntryValidatableObject<string>), propertiesDictionary["Title"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<int>), propertiesDictionary["Credits"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<int>), propertiesDictionary["DepartmentID"].GetType());
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_NoGroups()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            ).CreateFields();

            var item = Assert.Single(formLayout.ControlGroupBoxList);
            Assert.Equal(6, item.Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.Single().GroupHeader);
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_AllFieldsGrouped()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentFormWithAllItemsGrouped,
                typeof(DepartmentModel)
            ).CreateFields();

            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupTwo").Count);
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_SomeFieldsGrouped()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentFormWithSomesItemsGrouped,
                typeof(DepartmentModel)
            ).CreateFields();


            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Department").Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList[0].GroupHeader);
        }

        private IFieldsCollectionBuilder GetFieldsCollectionBuilder(DataFormSettingsDescriptor dataFormSettingsDescriptor, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetFieldsCollectionBuilder
            (
                modelType,
                dataFormSettingsDescriptor.FieldSettings,
                dataFormSettingsDescriptor,
                dataFormSettingsDescriptor.ValidationMessages,
                null,
                null
            );
        }
    }
}

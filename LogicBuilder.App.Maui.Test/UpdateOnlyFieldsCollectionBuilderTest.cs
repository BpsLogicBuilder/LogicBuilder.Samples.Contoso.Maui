using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Test.Helpers;
using LogicBuilder.App.Maui.Test.ScreenDescriptors;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using System.Collections.ObjectModel;

namespace LogicBuilder.App.Maui.Test
{
    public class UpdateOnlyFieldsCollectionBuilderTest
    {
        public UpdateOnlyFieldsCollectionBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapCourseModelToIValidatableList()
        {
            //arrange
            IUpdateOnlyFieldsCollectionBuilder updateOnlyFieldsCollectionBuilder = serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetUpdateOnlyFieldsCollectionBuilder
            (
                typeof(CourseModel),
                Descriptors.CourseForm.FieldSettings,
                Descriptors.CourseForm,
                Descriptors.CourseForm.ValidationMessages,
                null,
                null
            );

            //act
            ObservableCollection<IValidatable> properties = updateOnlyFieldsCollectionBuilder.CreateFields().Properties;

            //assert
            Dictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            Assert.Equal(typeof(LabelValidatableObject<int>), propertiesDictionary["CourseID"].GetType());
        }
    }
}

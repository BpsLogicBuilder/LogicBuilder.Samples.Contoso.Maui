using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Test.Helpers;
using LogicBuilder.App.Maui.Test.ScreenDescriptors;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicBuilder.App.Maui.Test
{
    public class ReadOnlyCollectionCellPropertiesUpdaterTest
    {
        public ReadOnlyCollectionCellPropertiesUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapInstructorModelToIReadOnlyList()
        {
            //arrange
            InstructorModel instructor = new()
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20, 0, 0, 0, DateTimeKind.Unspecified),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses =
                [
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                ]
            };

            List<ItemBindingDescriptor> itemBindings =
            [
                new TextItemBindingDescriptor
                (
                    name: "Header",
                    property: "OfficeAssignment.Location",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    fieldTypeSource: null,
                    title: ""
                ),
                new TextItemBindingDescriptor
                (
                    name: "Text",
                    property: "HireDate",
                    stringFormat: "{0:MMMM dd, yyyy}",
                    textTemplate: new TextFieldTemplateDescriptor("DateTemplate"),
                    fieldTypeSource: null,
                    title: ""
                ),
                new MultiSelectItemBindingDescriptor
                (
                    name: "Detail",
                    property: "Courses",
                    title: "",
                    stringFormat:"{0}",
                    multiSelectTemplate: Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings.OfType<MultiSelectFormControlSettingsDescriptor>().Single(f => f.Field == "Courses").MultiSelectTemplate,
                    keyFields: ["CourseID"],
                    fieldTypeSource: null,
                    listElementTypeSource: null
                )
            ];

            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                itemBindings,
                typeof(InstructorModel)
            ).CreateFields();

            //act
            serviceProvider.GetRequiredService<IReadOnlyCollectionCellPropertiesUpdater>().UpdateProperties
            (
                properties,
                typeof(InstructorModel),
                instructor,
                itemBindings
            );

            //assert
            Dictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal("Location1", propertiesDictionary["OfficeAssignment.Location"]);
            Assert.Equal(new DateTime(2021, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), propertiesDictionary["HireDate"]);
            Assert.Equal(1, ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]!).First().CourseID);
        }

        [Fact]
        public void MapCourseModelToIReadOnlyList()
        {
            //arrange
            CourseModel course = new()
            {
                CourseID = 3,
                Title = "Chemistry",
                Credits = 5
            };

            List<ItemBindingDescriptor> itemBindings =
            [
                new TextItemBindingDescriptor
                (
                    name: "Header",
                    property: "CourseID",
                    stringFormat: "{}",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    fieldTypeSource: null,
                    title: ""
                ),
                new TextItemBindingDescriptor
                (
                    name: "Detail",
                    property: "Title",
                    stringFormat: "{0}",
                    textTemplate: new TextFieldTemplateDescriptor("TextTemplate"),
                    fieldTypeSource: null,
                    title: ""
                ),
                new DropDownItemBindingDescriptor
                (
                    name: "Detail",
                    property: "Credits",
                    title: "",
                    stringFormat: "{0}",
                    requiresReload: false,
                    dropDownTemplate: Descriptors.CourseForm.FieldSettings.OfType<DropdownSelectorControlSettingsDescriptor>().Single(f => f.Field == "Credits").DropDownTemplate,
                    fieldTypeSource: null
                )
            ];

            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                itemBindings,
                typeof(CourseModel)
            )
            .CreateFields();

            //act
            serviceProvider.GetRequiredService<IReadOnlyCollectionCellPropertiesUpdater>().UpdateProperties
            (
                properties,
                typeof(CourseModel),
                course,
                itemBindings
            );

            //assert
            Dictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["CourseID"]);
            Assert.Equal("Chemistry", propertiesDictionary["Title"]);
            Assert.Equal(5, propertiesDictionary["Credits"]);
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

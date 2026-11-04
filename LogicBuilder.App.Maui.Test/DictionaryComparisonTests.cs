using LogicBuilder.App.Maui.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LogicBuilder.App.Maui.Test
{
    public class DictionaryComparisonTests
    {
        public DictionaryComparisonTests()
        {
            Initialize();
        }

        #region Fields
        private Dictionary<string, object?> first;
        private Dictionary<string, object?> second;
        #endregion Fields

        [Fact]
        public void ShouldReturnTrueForMatchingLists()
        {
            Dictionary<string, object?> firstDictionary = new()
            {
                ["list"] = new List<int> { 1, 2, 3 }
            };

            Dictionary<string, object?> secondDictionary = new()
            {
                ["list"] = new List<int> { 1, 2, 3 }
            };

            Assert.True(new DictionaryComparer().Equals(firstDictionary, secondDictionary));
        }

        [Fact]
        public void ShouldReturnFalseForListsOutOfOrder()
        {
            Dictionary<string, object?> firstDictionary = new()
            {
                ["list"] = new List<int> { 1, 2, 3 }
            };

            Dictionary<string, object?> secondDictionary = new()
            {
                ["list"] = new List<int> { 1, 3, 2 }
            };

            Assert.False(new DictionaryComparer().Equals(firstDictionary, secondDictionary));
        }

        [Fact]
        public void ShouldReturnTrueIfAllFieldsMatch()
        {
            Assert.True(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseValuesDoNotMatch()
        {
            first["ID"] = 3;
            second["ID"] = 4;
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseNestedValuesDoNotMatch()
        {
            ((Dictionary<string, object?>)first["OfficeAssignment"]!)["Location"] = "Location1";
            ((Dictionary<string, object?>)second["OfficeAssignment"]!)["Location"] = "Location2";
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseForMisMatchInAChildCollectionValue()
        {
            var firstCoursesList = (List<Dictionary<string, object?>>)first["Courses"]!;
            var secondCoursesList = (List<Dictionary<string, object?>>)first["Courses"]!;

            firstCoursesList[0]["CourseTitle"] = "Chemistry";
            secondCoursesList[0]["CourseTitle"] = "Chem";
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [Fact]
        public void ShouldReturnFalseForMisMatchInAChildCollectionCount()
        {
            var firstCoursesList = (List<Dictionary<string, object?>>)first["Courses"]!;
            firstCoursesList.Add([]);
            Assert.False(new DictionaryComparer().Equals(first, second));
        }

        [MemberNotNull(nameof(first), nameof(second))]
        private void Initialize()
        {
            first = new Dictionary<string, object?>
            {
                ["ID"] = 3,
                ["FirstName"] = "John",
                ["LastName"] = "Smith",
                ["HireDate"] = new DateTime(2021, 5, 20, 0, 0, 0, DateTimeKind.Unspecified),
                ["OfficeAssignment"] = new Dictionary<string, object?>
                {
                    ["Location"] = "Location1"
                },
                ["Courses"] = new List<Dictionary<string, object?>>
                {
                    new()
                    {
                        ["CourseID"] = 1,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Chemistry"
                    },
                    new()
                    {
                        ["CourseID"] = 2,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Physics"
                    },
                    new()
                    {
                        ["CourseID"] = 3,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Mathematics"
                    }
                }
            };

            second = new Dictionary<string, object?>
            {
                ["ID"] = 3,
                ["FirstName"] = "John",
                ["LastName"] = "Smith",
                ["HireDate"] = new DateTime(2021, 5, 20, 0, 0, 0, DateTimeKind.Unspecified),
                ["OfficeAssignment"] = new Dictionary<string, object?>
                {
                    ["Location"] = "Location1"
                },
                ["Courses"] = new List<Dictionary<string, object?>>
                {
                    new()
                    {
                        ["CourseID"] = 1,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Chemistry"
                    },
                    new()
                    {
                        ["CourseID"] = 2,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Physics"
                    },
                    new()
                    {
                        ["CourseID"] = 3,
                        ["InstructorID"] = 3,
                        ["CourseTitle"] = "Mathematics"
                    }
                }
            };
        }
    }
}

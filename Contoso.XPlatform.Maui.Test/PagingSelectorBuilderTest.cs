using Contoso.Domain.Entities;
using Contoso.XPlatform.Maui.Test.Helpers;
using Contoso.XPlatform.Maui.Test.ScreenDescriptors;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Utils.Interfaces;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Maui.Test
{
    public class PagingSelectorBuilderTest
    {
        public PagingSelectorBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public async Task CanCreatePagingSelectorDescriptorWithoutFilter()
        {
            //arrange
            IMappingOperations mappingOperations = serviceProvider.GetRequiredService<IMappingOperations>();

            //act
            SelectorLambdaDescriptor selectorLambdaDescriptor = await serviceProvider.GetRequiredService<IPagingSelectorBuilder>().CreateSelector
            (
                10,
                "",
                SearchFormDescriptors.SudentsForm.CreatePagingSelectorFlowName
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)mappingOperations.MapToOperator(selectorLambdaDescriptor);
            Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>> selector = (Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "$it => $it.OrderBy(o => o.FullName).Skip(10).Take(2)"
            );
        }

        [Fact]
        public async Task CanCreatePagingSelectorDescriptorWithFilter()
        {
            //arrange
            IMappingOperations mappingOperations = serviceProvider.GetRequiredService<IMappingOperations>();

            //act
            SelectorLambdaDescriptor selectorLambdaDescriptor = await serviceProvider.GetRequiredService<IPagingSelectorBuilder>().CreateSelector
            (
                10,
                "Joh",
                SearchFormDescriptors.SudentsForm.CreatePagingSelectorFlowName
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)mappingOperations.MapToOperator(selectorLambdaDescriptor);
            Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>> selector = (Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "$it => $it.Where(f => (f.EnrollmentDateString.Contains(\"Joh\") OrElse (f.FirstName.Contains(\"Joh\") OrElse f.LastName.Contains(\"Joh\")))).OrderBy(o => o.FullName).Skip(10).Take(2)"
            );
        }

        private static void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            AssertStringIsCorrect(ExpressionStringBuilder.ToString(expression));

            void AssertStringIsCorrect(string resultExpression)
                => Assert.True
                (
                    expected == resultExpression,
                    $"Expected expression '{expected}' but the deserializer produced '{resultExpression}'"
                );
        }
    }
}

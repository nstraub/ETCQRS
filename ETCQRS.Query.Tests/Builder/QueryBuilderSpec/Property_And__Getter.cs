using System;
using System.Linq.Expressions;

using ETCQRS.Query.Builder;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryBuilderSpec
{
    [TestFixture]
    public class Property_And__Getter
    {
        [Test]
        public void IT_SHOULD_SET_BULDER_QUERY_LINKER_TO_AND_ALSO_EXPRESSION ()
        {
            var builder = new QueryExpressionBuilder {QueryLinker = Expression.OrElse};
            
            // ReSharper disable once UnusedVariable
            var queryBuildDirector = builder.And;
            Assert.AreEqual((Func<Expression, Expression, BinaryExpression>)Expression.AndAlso, builder.QueryLinker);
        }
    }
}


using System;
using System.Linq.Expressions;

using ETCQRS.Query.Builder;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryBuilderSpec

{
    [TestFixture]
    public class Property_QueryLinker__Setter
    {
        [Test]
        public void IT_SHOULD_ONLY_ALLOW_AND_ALSO_AND_OR_ELSE_EXPRESSIONS_TO_BE_SET ()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => new QueryExpressionBuilder().QueryLinker = Expression.Equal);
            Assert.AreEqual("Only conditional logic operators, \"AndAlso\" and \"OrElse\" are allowed", exception.Message);
        }
    }
}


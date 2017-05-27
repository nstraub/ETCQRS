using System;
using System.Linq.Expressions;

using ETCQRS.Query.Builder;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryExpressionBuilderSpec

{
    [TestFixture]
    public class Property_QueryLinker__Setter
    {
        [Test]
        public void IT_SHOULD_ONLY_ALLOW_AND_ALSO_AND_OR_ELSE_EXPRESSIONS_TO_BE_SET ()
        {
            Assert.That(() => new QueryExpressionBuilder().QueryLinker = Expression.Equal, Throws.Exception.TypeOf < InvalidOperationException >  ().With.Message.EqualTo("Only conditional logic operators, \"AndAlso\" and \"OrElse\" are allowed"));
        }
    }
}


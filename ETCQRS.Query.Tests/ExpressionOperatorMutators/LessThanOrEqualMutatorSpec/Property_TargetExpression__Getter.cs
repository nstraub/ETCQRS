using System;
using System.Linq.Expressions;

using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Util;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Property_TargetExpression__Getter
    {
        [Test]
        public void IT_SHOULD_RETURN_A_LESS_THAN_OR_EQUALS_EXPRESSION ()
        {
            Assert.AreEqual((Func<Expression, Expression, BinaryExpression>)Expression.LessThanOrEqual, new LessThanOrEqualMutator(new MutatorFlyweightFactory()).GetTargetExpression());
        }
    }
}


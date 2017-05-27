using System;
using System.Linq.Expressions;

using ETCQRS.Query.ExpressionOperatorMutator;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GreaterThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Property_TargetExpression__Getter : TestsFor<GreaterThanOrEqualMutator>
    {
        [Test]
        public void IT_SHOULD_RETURN_A_GREATER_THAN_OR_EQUAL_EXPRESSION ()
        {
            Assert.That(Subject.GetTargetExpression(), Is.EqualTo((Func<Expression, Expression, BinaryExpression>)Expression.GreaterThanOrEqual));
        }
    }
}


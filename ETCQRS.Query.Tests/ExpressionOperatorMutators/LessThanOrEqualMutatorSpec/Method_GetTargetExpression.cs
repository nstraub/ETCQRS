using System;
using System.Linq.Expressions;

using ETCQRS.Query.ExpressionOperatorMutator;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Method_GetTargetExpression : TestsFor<LessThanOrEqualMutator>
    {
        [Test]
        public void IT_SHOULD_RETURN_A_LESS_THAN_OR_EQUALS_EXPRESSION ()
        {
            Assert.That(Subject.GetTargetExpression(), Is.EqualTo((Func<Expression, Expression, BinaryExpression>)Expression.LessThanOrEqual));
        }
    }
}


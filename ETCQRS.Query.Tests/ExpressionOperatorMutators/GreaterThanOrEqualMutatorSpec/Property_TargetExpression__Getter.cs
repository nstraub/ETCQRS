using System;
using System.Linq.Expressions;

using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GreaterThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Property_TargetExpression__Getter : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_RETURN_A_GREATER_THAN_OR_EQUAL_EXPRESSION ()
        {
            Assert.AreEqual((Func<Expression, Expression, BinaryExpression>)Expression.GreaterThanOrEqual, Kernel.Get<GreaterThanOrEqualMutator>().GetTargetExpression());
        }
    }
}


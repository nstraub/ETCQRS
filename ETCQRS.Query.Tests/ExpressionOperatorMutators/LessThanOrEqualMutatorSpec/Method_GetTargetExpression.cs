using System;
using System.Linq.Expressions;

using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Method_GetTargetExpression : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_RETURN_A_LESS_THAN_OR_EQUALS_EXPRESSION ()
        {
            Assert.AreEqual((Func<Expression, Expression, BinaryExpression>)Expression.LessThanOrEqual, Kernel.Get<LessThanOrEqualMutator>().GetTargetExpression());
        }
    }
}


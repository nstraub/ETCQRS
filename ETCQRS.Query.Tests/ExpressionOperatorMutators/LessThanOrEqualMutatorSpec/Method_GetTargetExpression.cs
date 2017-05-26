﻿using System;
using System.Linq.Expressions;

using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Factories;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Method_GetTargetExpression
    {
        [Test]
        public void IT_SHOULD_RETURN_A_LESS_THAN_OR_EQUALS_EXPRESSION ()
        {
            Assert.AreEqual((Func<Expression, Expression, BinaryExpression>)Expression.LessThanOrEqual, new LessThanOrEqualMutator(new MutatorFlyweightFactory()).GetTargetExpression());
        }
    }
}

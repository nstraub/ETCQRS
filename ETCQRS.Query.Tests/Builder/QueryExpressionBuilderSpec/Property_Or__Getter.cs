﻿using System;
using System.Linq.Expressions;

using ETCQRS.Query.Builder;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryExpressionBuilderSpec

{
    [TestFixture]
    public class Property_Or__Getter
    {
        [Test]
        public void IT_SHOULD_SET_BULDER_QUERY_LINKER_TO_OR_ELSE_EXPRESSION ()
        {
            var builder = new QueryExpressionBuilder();

            // ReSharper disable once UnusedVariable
            var queryBuildDirector = builder.Or;
            Assert.That(builder.QueryLinker, Is.EqualTo((Func<Expression, Expression, BinaryExpression>)Expression.OrElse));
        }
    }
}


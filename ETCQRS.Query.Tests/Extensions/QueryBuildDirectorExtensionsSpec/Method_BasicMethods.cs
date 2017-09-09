using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Extensions;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Extensions.QueryBuildDirectorExtensionsSpec
{
    /**
     * These methods provide convenient shortcuts for the allowed expressions,
     * discouraging the user from accessing the AddExpression manually to avoid inserting
     * dissalowed expressions.
     */
    [TestFixture]
    public class Method_BasicMethods
    {
        private Mock<IQueryExpressionBuilder> _expressionBuilderMock;
        private IQueryExpressionBuilder _expressionBuilder;

        [SetUp]
        public void SetupTest ()
        {
            _expressionBuilderMock = new Mock<IQueryExpressionBuilder>();
            _expressionBuilder = _expressionBuilderMock.Object;
            _expressionBuilderMock.SetupGet(d => d.And).Returns(_expressionBuilder);
            _expressionBuilderMock.Setup(d => d.AddExpression(It.IsAny<Func<Expression, Expression, BinaryExpression>>(), "test")).Returns(_expressionBuilder);
            _expressionBuilderMock.Setup(d => d.Mutate()).Returns(_expressionBuilder);
        }

        [Test]
        public void EQUAL_METHOD ()
        {
            QueryBuildDirectorExtensions.Equal(_expressionBuilder, "test");
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.Equal, "test"), Times.Once());
        }

        [Test]
        public void NOT_EQUAL_METHOD ()
        {
            QueryBuildDirectorExtensions.NotEqual(_expressionBuilder, "test");
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.NotEqual, "test"), Times.Once());
        }

        [Test]
        public void GREATER_THAN_METHOD ()
        {
            QueryBuildDirectorExtensions.IsGreaterThan(_expressionBuilder, "test");
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.GreaterThan, "test"), Times.Once());
        }

        [Test]
        public void LESS_THAN_METHOD ()
        {
            QueryBuildDirectorExtensions.IsLessThan(_expressionBuilder, "test");
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.LessThan, "test"), Times.Once());
        }

        [Test]
        public void IN_OPEN_INTERVAL_METHOD ()
        {
            QueryBuildDirectorExtensions.InOpenInterval(_expressionBuilder, "test", "test");
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.GreaterThan, "test"), Times.Once());
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.LessThan, "test"), Times.Once());
        }

        [Test]
        public void IN_CLOSED_INTERVAL_METHOD ()
        {
            QueryBuildDirectorExtensions.InClosedInterval(_expressionBuilder, "test", "test");

            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.GreaterThan, "test"), Times.Once());
            _expressionBuilderMock.Verify(d => d.AddExpression(Expression.LessThan, "test"), Times.Once());
            _expressionBuilderMock.Verify(d => d.Mutate(), Times.Exactly(2));
        }
    }
}


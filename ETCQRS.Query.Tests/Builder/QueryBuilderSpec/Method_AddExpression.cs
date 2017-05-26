using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Builder;
using ETCQRS.Query.Tests.Fakes;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryBuilderSpec
{
    /**
     * Adds an expression query on passed descriptor.
     * it should:
     *      - Set query on descriptor when query is null.
     *      - Add query to existing query on descriptor when it is not null.
     *      - Compose existing query with AndAlso when linker is AndAlso
     *      - Compose existing query with OrElse when linker is OrElse
     *      - Throw exception if descriptor's property expression is null.
     *      - Notify any subscribed observers.
     * 
     */
    [TestFixture]
    public class Method_AddExpression
    {
        private QueryExpressionBuilder _queryExpressionBuilder;
        private Mock<IQueryDescriptor> _descriptorMock;
        private IQueryDescriptor _descriptor;
        private ParameterExpression _parameter;
        private MemberExpression _property;

        [SetUp]
        public void TestSetup ()
        {
            _queryExpressionBuilder = new QueryExpressionBuilder();
            _descriptorMock = new Mock<IQueryDescriptor>();
            _parameter = Expression.Parameter(typeof(FakeEntity));
            _descriptorMock.SetupGet(d => d.Parameter).Returns(_parameter);
            _property = Expression.Property(_parameter, "Test");
            _descriptorMock.SetupGet(d => d.PropertyExpression).Returns(_property);
            _descriptor = _descriptorMock.Object;
            _descriptorMock.SetupProperty(d => d.Query);
            _queryExpressionBuilder.Descriptor = _descriptor;
        }

        [Test]
        public void IT_SHOULD_SET_QUERY_ON_DESCRIPTOR_WHEN_NULL ()
        {
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            Assert.AreEqual(ExpressionType.Equal,_descriptor.Query.NodeType);
            Assert.AreEqual("test", ((ConstantExpression)_descriptor.Query.Right).Value);

            Assert.AreSame(_parameter, ((MemberExpression)_descriptor.Query.Left).Expression);
            Assert.AreEqual("Test", ((MemberExpression)_descriptor.Query.Left).Member.Name);
        }

        [Test]
        public void IT_SHOULD_ADD_QUERY_TO_DESCRIPTOR_WHEN_NOT_NULL ()
        {
            var initialQuery = Expression.Equal(_property, Expression.Constant("test"));

            _descriptor.Query = initialQuery;
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            var result = _descriptor.Query;
            var resultLeft = (BinaryExpression)result.Left;
            var resultRight = (BinaryExpression)result.Right;


            Assert.AreSame(initialQuery, resultLeft);
            Assert.AreEqual(ExpressionType.Equal, resultRight.NodeType);
            Assert.AreEqual("test", ((ConstantExpression)resultRight.Right).Value);

            Assert.AreSame(_parameter, ((MemberExpression)resultRight.Left).Expression);
            Assert.AreEqual("Test", ((MemberExpression)resultRight.Left).Member.Name);
        }

        [Test]
        public void IT_SHOULD_COMPOSE_RESULTING_QUERY_WITH_AND_ALSO_WHEN_LINKER_IS_AND_ALSO ()
        {
            var initialQuery = Expression.Equal(_property, Expression.Constant("test"));

            _descriptor.Query = initialQuery;
            _queryExpressionBuilder.QueryLinker = Expression.AndAlso;
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            Assert.AreEqual(ExpressionType.AndAlso, _descriptor.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_COMPOSE_RESULTING_QUERY_WITH_OR_ELSE_WHEN_LINKER_IS_OR_ELSE ()
        {
            var initialQuery = Expression.Equal(_property, Expression.Constant("test"));

            _descriptor.Query = initialQuery;
            _queryExpressionBuilder.QueryLinker = Expression.OrElse;
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            Assert.AreEqual(ExpressionType.OrElse, _descriptor.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_THROW_NULL_REFERENCE_EXCEPTION_IF_DESCRIPTORS_PROPERTY_EXPRESSION_IS_NULL ()
        {
            _descriptorMock.SetupGet(d => d.PropertyExpression).Returns(() => null);
            var exception = Assert.Throws<NullReferenceException>(() => _queryExpressionBuilder.AddExpression(Expression.Equal, "test"));

            Assert.AreEqual("You must provide a property to compare the value to", exception.Message);
        }

        [Test]
        public void IT_SHOULD_NOTIFY_ANY_SUBSCRIBED_OBSERVERS ()
        {
            var observerMock = new Mock<IObserver>();
            _queryExpressionBuilder.Subscribe(observerMock.Object);
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            observerMock.Verify(o => o.Update(It.IsAny<BinaryExpression>()), Times.Once);

        }
    }
}


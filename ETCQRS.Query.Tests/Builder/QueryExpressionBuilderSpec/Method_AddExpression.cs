using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Builder;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Moq;

using Ninject.MockingKernel.Moq;

using NUnit.Framework;
using Ninject;

namespace ETCQRS.Query.Tests.Builder.QueryExpressionBuilderSpec
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
    public class Method_AddExpression : NinjectFixture
    {
        private QueryExpressionBuilder _queryExpressionBuilder;
        private IQueryDescriptor _descriptor;

        [OneTimeSetUp]
        public void FixtureSetup ()
        {
            Kernel.Load(new InitialQueryGraph(Expression.Equal(GetProperty(), Expression.Constant("test"))));
        }

        [SetUp]
        public void TestSetup ()
        {
            var descriptorMock = Kernel.GetMock<IQueryDescriptor>();
            descriptorMock.SetupGet(d => d.Parameter).Returns(Kernel.Get<ParameterExpression>());
            descriptorMock.SetupGet(d => d.Property).Returns(Kernel.Get<MemberExpression>());
            descriptorMock.SetupProperty(d => d.Query);

            Kernel.GetMock<QueryExpressionBuilder>().CallBase = true;

            _queryExpressionBuilder = Kernel.Get<QueryExpressionBuilder>();

            _descriptor = Kernel.Get<IQueryDescriptor>();
            _queryExpressionBuilder.Descriptor = _descriptor;
        }

        [Test]
        public void IT_SHOULD_SET_QUERY_ON_DESCRIPTOR_WHEN_NULL ()
        {
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            Assert.AreEqual(ExpressionType.Equal,_descriptor.Query.NodeType);
            Assert.AreEqual("test", ((ConstantExpression)_descriptor.Query.Right).Value);

            Assert.AreSame(GetParameter(), ((MemberExpression)_descriptor.Query.Left).Expression);
            Assert.AreEqual("Test", ((MemberExpression)_descriptor.Query.Left).Member.Name);
        }

        [Test]
        public void IT_SHOULD_ADD_QUERY_TO_DESCRIPTOR_WHEN_NOT_NULL ()
        {
            _descriptor.Query = GetQuery("Initial");
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            var result = _descriptor.Query;
            var resultLeft = (BinaryExpression)result.Left;
            var resultRight = (BinaryExpression)result.Right;


            Assert.AreSame(GetQuery("Initial"), resultLeft);
            Assert.AreEqual(ExpressionType.Equal, resultRight.NodeType);
            Assert.AreEqual("test", ((ConstantExpression)resultRight.Right).Value);

            var property = ((MemberExpression)resultRight.Left);

            Assert.AreSame(GetParameter(), property.Expression);
            Assert.AreEqual("Test", property.Member.Name);
        }

        [Test]
        public void IT_SHOULD_COMPOSE_RESULTING_QUERY_WITH_AND_ALSO_WHEN_LINKER_IS_AND_ALSO ()
        {
            _descriptor.Query = GetQuery("Initial");
            _queryExpressionBuilder.QueryLinker = Expression.AndAlso;
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            Assert.AreEqual(ExpressionType.AndAlso, _descriptor.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_COMPOSE_RESULTING_QUERY_WITH_OR_ELSE_WHEN_LINKER_IS_OR_ELSE ()
        {
            _descriptor.Query = GetQuery("Initial");
            _queryExpressionBuilder.QueryLinker = Expression.OrElse;
            _queryExpressionBuilder.AddExpression(Expression.Equal, "test");

            Assert.AreEqual(ExpressionType.OrElse, _descriptor.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_THROW_NULL_REFERENCE_EXCEPTION_IF_DESCRIPTORS_PROPERTY_EXPRESSION_IS_NULL ()
        {
            Kernel.GetMock<IQueryDescriptor>().SetupGet(d => d.Property).Returns(() => null);
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


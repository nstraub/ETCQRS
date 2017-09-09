using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Builder;
using ETCQRS.Query.Tests.SUTUtils;

using Moq;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.Builder.QueryExpressionBuilderSpec
{
    [TestFixture]
    public class Method_AddExpression : TestsFor<QueryExpressionBuilder>
    {
        private IQueryDescriptor _descriptor;
        private BinaryExpression _query = Expression.Equal(Expression.Constant(3), Expression.Constant(3));

        [SetUp]
        public void TestSetup ()
        {
            var descriptorMock = GetMock<IQueryDescriptor>();

            var parameter = Expression.Parameter(typeof(FakeEntity));
            Set.Mock<ParameterExpression>().To(parameter);
            Set.Mock<MemberExpression>().To(Expression.Property(parameter, "Test"));

            descriptorMock.SetupGet(d => d.Parameter).Returns(Get<ParameterExpression>());
            descriptorMock.SetupGet(d => d.Property).Returns(Get<MemberExpression>());
            descriptorMock.SetupProperty(d => d.Query);

            _descriptor = GetMock<IQueryDescriptor>().Object;
            Subject.Descriptor = _descriptor;
        }

        [Test]
        public void IT_SHOULD_SET_QUERY_ON_DESCRIPTOR_WHEN_NULL ()
        {
            Subject.AddExpression(Expression.Equal, "test");

            Assert.That(_descriptor.Query.NodeType, Is.EqualTo(ExpressionType.Equal));
            Assert.That(((ConstantExpression)_descriptor.Query.Right).Value, Is.EqualTo("test"));

            Assert.That(((MemberExpression)_descriptor.Query.Left).Expression, Is.SameAs(GetParameter()));
            Assert.That(((MemberExpression)_descriptor.Query.Left).Member.Name, Is.EqualTo("Test"));
        }

        private ParameterExpression GetParameter ()
        {
            return Get<ParameterExpression>();
        }

        [Test]
        public void IT_SHOULD_ADD_QUERY_TO_DESCRIPTOR_WHEN_NOT_NULL ()
        {
            _descriptor.Query = GetQuery();
            Subject.AddExpression(Expression.Equal, "test");

            var result = _descriptor.Query;
            var resultLeft = (BinaryExpression)result.Left;
            var resultRight = (BinaryExpression)result.Right;


            Assert.That(resultLeft, Is.SameAs(GetQuery()));
            Assert.That(resultRight.NodeType, Is.EqualTo(ExpressionType.Equal));
            Assert.That(((ConstantExpression)resultRight.Right).Value, Is.EqualTo("test"));

            var property = ((MemberExpression)resultRight.Left);

            Assert.That(property.Expression, Is.SameAs(GetParameter()));
            Assert.That(property.Member.Name, Is.EqualTo("Test"));
        }

        private BinaryExpression GetQuery ()
        {
            return _query;
        }

        [Test]
        public void IT_SHOULD_COMPOSE_RESULTING_QUERY_WITH_AND_ALSO_WHEN_LINKER_IS_AND_ALSO ()
        {
            _descriptor.Query = GetQuery();
            Subject.QueryLinker = Expression.AndAlso;
            Subject.AddExpression(Expression.Equal, "test");

            Assert.That(_descriptor.Query.NodeType, Is.EqualTo(ExpressionType.AndAlso));
        }

        [Test]
        public void IT_SHOULD_COMPOSE_RESULTING_QUERY_WITH_OR_ELSE_WHEN_LINKER_IS_OR_ELSE ()
        {
            _descriptor.Query = GetQuery();
            Subject.QueryLinker = Expression.OrElse;
            Subject.AddExpression(Expression.Equal, "test");

            Assert.That(_descriptor.Query.NodeType, Is.EqualTo(ExpressionType.OrElse));
        }

        [Test]
        public void IT_SHOULD_THROW_NULL_REFERENCE_EXCEPTION_IF_DESCRIPTORS_PROPERTY_EXPRESSION_IS_NULL ()
        {
            GetMock<IQueryDescriptor>().SetupGet(d => d.Property).Returns(() => null);
            Assert.That(() => Subject.AddExpression(Expression.Equal, "test"), Throws.Exception.TypeOf<NullReferenceException>().With.Message.EqualTo("You must provide a property to compare the value to"));
            
        }

        [Test]
        public void IT_SHOULD_NOTIFY_ANY_SUBSCRIBED_OBSERVERS ()
        {
            var observerMock = new Mock<IObserver>();
            Subject.Subscribe(observerMock.Object);
            Subject.AddExpression(Expression.Equal, "test");

            observerMock.Verify(o => o.Update(It.IsAny<BinaryExpression>()), Times.Once());

        }
    }
}


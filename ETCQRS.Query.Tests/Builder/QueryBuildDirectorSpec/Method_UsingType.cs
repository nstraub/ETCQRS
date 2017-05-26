using System;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Builder;
using ETCQRS.Query.Tests.Fakes;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryBuildDirectorSpec
{
    /*
     * Creates a new Query Descriptor using the passed type.
     * It should:
     *      - Create a new Query Descriptor using the passed type.
     *      - Throw an exception if there is already an active query and passed type is not the same
     *      - Not perform any operation if passed type is same as curent query's type
     */
    [TestFixture]
    public class Method_UsingType
    {
        private IQueryBuildFacade _facade;
        private Mock<IQueryExpressionBuilder> _builderMock;
        private Mock<IQueryDescriptorFactory> _factoryMock;
        private Mock<IQueryDescriptor> _descriptorMock;
        private Mock<IObserver> _observerMock;


        [SetUp]
        public void SetupTest ()
        {
            _factoryMock = new Mock<IQueryDescriptorFactory>();
            _builderMock = new Mock<IQueryExpressionBuilder>();
            _descriptorMock = new Mock<IQueryDescriptor>();
            _observerMock = new Mock<IObserver>();

            _factoryMock.Setup(f => f.Create(typeof(FakeEntity))).Returns(_descriptorMock.Object);
            _factoryMock.Setup(f => f.CreateMutatorObserver(_descriptorMock.Object)).Returns(_observerMock.Object);

            _facade = new QueryBuildFacade(_factoryMock.Object, _builderMock.Object, new QueryComposite());
        }

        [Test]
        public void IT_SHOULD_CREATE_A_NEW_QUERY_DESCRIPTOR_USING_THE_PASSED_TYPE ()
        {
            _facade.UsingType(typeof(FakeEntity));
            _factoryMock.Verify();
        }

        [Test]
        public void IT_SHOULD_THROW_AN_EXCEPTION_IF_THERE_IS_ALREADY_AN_ACTIVE_QUERY_AND_PASSED_TYPE_IS_NOT_SAME ()
        {
            _facade.UsingType(typeof(FakeEntity));

            var exception = Assert.Throws<InvalidOperationException>(() => _facade.UsingType(typeof(OtherFakeEntity)));
            Assert.AreEqual("You are already building an expression. Finalize it by invoking the appropriate AddExpression method before creating a new one", exception.Message);
        }

        [Test]
        public void IT_SHOULD_NOT_PERFORM_ANY_OPERATION_IF_PASSED_TYPE_IS_SAME_AS_CURRENT_QUERY ()
        {
            _facade.UsingType(typeof(FakeEntity));
            _facade.UsingType(typeof(FakeEntity));

            _factoryMock.Verify(f => f.Create(typeof(FakeEntity)), Times.AtMostOnce);
        }

        [Test]
        public void IT_SHOULD_SUBSCRIBE_A_MUTATOR_OBSERVER_ON_THE_BUILDER ()
        {
            _facade.UsingType(typeof(FakeEntity));
            _builderMock.Verify(b => b.Subscribe(_observerMock.Object), Times.Once);
        }
    }
}


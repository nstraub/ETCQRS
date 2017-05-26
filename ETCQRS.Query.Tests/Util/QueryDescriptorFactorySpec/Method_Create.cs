using System;

using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Tests.Fakes;
using ETCQRS.Query.Util;

using NUnit.Framework;
using Moq;

namespace ETCQRS.Query.Tests.Util.QueryDescriptorFactorySpec

{
    [TestFixture]
    public class Method_Create
    {
        [Test]
        public void IT_SHOULD_CREATE_A_NEW_QUERY_DESCRIPTOR_WITH_PROVIDED_TYPE_AS_PARAMETER ()
        {
            var mutatorFlyweightFactoryMock = new Mock<IFlyweightFactory<IExpressionMutator>>(MockBehavior.Strict);
            mutatorFlyweightFactoryMock.Setup(ff => ff.Get("Throwing")).Returns(new Mock<IExpressionMutator>().Object);

            var factory = new QueryDescriptorFactory(mutatorFlyweightFactoryMock.Object);
            var queryDescriptor = factory.Create(typeof(FakeEntity));
            Assert.AreEqual(typeof(FakeEntity), queryDescriptor.Parameter.Type);
        }
    }
}


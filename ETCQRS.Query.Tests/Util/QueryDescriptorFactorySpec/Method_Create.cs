using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Factories;
using ETCQRS.Query.Tests.SUTUtils;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.Util.QueryDescriptorFactorySpec

{
    [TestFixture]
    public class Method_Create : TestsFor<QueryDescriptorFactory>
    {

        [Test]
        public void IT_SHOULD_CREATE_A_NEW_QUERY_DESCRIPTOR_WITH_PROVIDED_TYPE_AS_PARAMETER ()
        {
            var mutatorFlyweightFactoryMock = GetMock<IFlyweightFactory<IExpressionMutator>>();
            mutatorFlyweightFactoryMock.Setup(ff => ff.Get("Throwing")).Returns(GetMock<IExpressionMutator>().Object);

            var queryDescriptor = Subject.Create(typeof(FakeEntity));

            Assert.That(queryDescriptor.Parameter.Type, Is.EqualTo(typeof(FakeEntity)));
            mutatorFlyweightFactoryMock.Verify();
        }
    }
}


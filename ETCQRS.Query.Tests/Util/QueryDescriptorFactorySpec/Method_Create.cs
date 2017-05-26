using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Factories;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using NUnit.Framework;

using Ninject;
using Ninject.MockingKernel.Moq;


namespace ETCQRS.Query.Tests.Util.QueryDescriptorFactorySpec

{
    [TestFixture]
    public class Method_Create : NinjectFixture
    {

        [Test]
        public void IT_SHOULD_CREATE_A_NEW_QUERY_DESCRIPTOR_WITH_PROVIDED_TYPE_AS_PARAMETER ()
        {
            var mutatorFlyweightFactoryMock = Kernel.GetMock<MutatorFlyweightFactory>();
            mutatorFlyweightFactoryMock.Setup(ff => ff.Get("Throwing")).Returns(Kernel.Get<IExpressionMutator>());

            var factory = Kernel.Get<QueryDescriptorFactory>();

            var queryDescriptor = factory.Create(typeof(FakeEntity));

            Assert.AreEqual(typeof(FakeEntity), queryDescriptor.Parameter.Type);
            mutatorFlyweightFactoryMock.Verify();
        }
    }
}


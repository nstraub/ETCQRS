using System.Collections.Generic;

using ETCQRS.Query.Factories;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Util.MutatorFlyweightFactorySpec

{
    [TestFixture]
    public class Method_Get : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_THROW_A_KEY_NOT_FOUND_EXCEPTION_WHICH_SPECIFIES_GIVEN_KEY_IF_NON_EXISTENT_KEY_IS_PASSED()
        {
            Kernel.GetMock<MutatorFlyweightFactory>().CallBase = true;

            var exception = Assert.Throws<KeyNotFoundException>(() => Kernel.Get<MutatorFlyweightFactory>().Get("invalid"));
            Assert.AreEqual("The given key, \"invalid\" was not present in the dictionary.", exception.Message);
            Assert.IsInstanceOf<KeyNotFoundException>(exception.InnerException);
        }
    }
}


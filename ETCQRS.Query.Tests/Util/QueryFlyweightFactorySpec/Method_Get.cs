using System;
using System.Collections.Generic;

using ETCQRS.Query.Factories;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Util.QueryFlyweightFactorySpec

{
    [TestFixture]
    public class Method_Get : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_RETURN_THE_CORRESPONDING_TYPE_WHEN_A_VALID_KEY_IS_PASSED ()
        {
            var queryType = new QueryFlyweightFactory(new List<Type>()
                                      {
                                          typeof(FakeQuery)
                                      }).Get("FakeQuery");

            Assert.AreEqual(typeof(FakeQuery), queryType.GetType());
        }
        [Test]
        public void IT_SHOULD_THROW_A_KEY_NOT_FOUND_EXCEPTION_WHICH_SPECIFIES_GIVEN_KEY_IF_NON_EXISTENT_KEY_IS_PASSED ()
        {
            var exception = Assert.Throws<KeyNotFoundException>(() => new QueryFlyweightFactory(new List<Type>()).Get("test"));
            Assert.AreEqual("The given key, \"test\" was not present in the dictionary.", exception.Message);
            Assert.IsInstanceOf<KeyNotFoundException>(exception.InnerException);
        }
    }
}


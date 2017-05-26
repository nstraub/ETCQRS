using System.Collections.Generic;

using ETCQRS.Query.Factories;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Util.MutatorFlyweightFactorySpec

{
    [TestFixture]
    public class Method_Get
    {
        [Test]
        public void IT_SHOULD_THROW_A_KEY_NOT_FOUND_EXCEPTION_WHICH_SPECIFIES_GIVEN_KEY_IF_NON_EXISTENT_KEY_IS_PASSED()
        {
            var exception = Assert.Throws<KeyNotFoundException>(() => new MutatorFlyweightFactory().Get("invalid"));
            Assert.AreEqual("The given key, \"invalid\" was not present in the dictionary.", exception.Message);
            Assert.IsInstanceOf<KeyNotFoundException>(exception.InnerException);
        }
    }
}


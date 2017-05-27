using System.Collections.Generic;

using ETCQRS.Query.Factories;
using ETCQRS.Query.Tests.SUTUtils;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.Util.QueryFlyweightFactorySpec

{
    [TestFixture]
    public class Method_Get : TestsFor<QueryFlyweightFactory>
    {
        [Test]
        public void IT_SHOULD_RETURN_THE_CORRESPONDING_TYPE_WHEN_A_VALID_KEY_IS_PASSED ()
        {
            AddInstance(typeof(FakeQuery));

            var queryType = Subject.Get("FakeQuery");

            Assert.That(queryType, Is.InstanceOf<FakeQuery>());
        }
        [Test]
        public void IT_SHOULD_THROW_A_KEY_NOT_FOUND_EXCEPTION_WHICH_SPECIFIES_GIVEN_KEY_IF_NON_EXISTENT_KEY_IS_PASSED ()
        {
            Assert.That(() => Subject.Get("invalid"), Throws.Exception.TypeOf<KeyNotFoundException>().With.Message.EqualTo("The given key, \"invalid\" is not present in the dictionary.").And.InnerException.InstanceOf<KeyNotFoundException>());
        }
    }
}


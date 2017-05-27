using System;

using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Util;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Util.EnsureSpec
{
    [TestFixture]
    public class Method_IsNotNull
    {
        [Test]
        public void IT_SHOULD_THROW_NULL_REFERENCE_EXCEPTION_IF_VALUE_IS_NULL ()
        {
            Assert.That(() => Ensure.IsNotNull<object>(null, "test message"), Throws.Exception.TypeOf<NullReferenceException>().With.Message.EqualTo("test message"));
        }

        [Test]
        public void IT_SHOULD_DO_NOTHING_IF_VALUE_IS_NOT_NULL ()
        {
            Assert.That(() => Ensure.IsNotNull(new FakeEntity(), "test message"), Throws.Nothing);
        }
    }
}


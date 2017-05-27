using ETCQRS.Query.Util;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Util.EnsureSpec
{
    [TestFixture]
    public class Method_ValidOperation
    {
        [Test]
        public void IT_SHOULD_THROW_INVALID_OPERATION_EXCEPTION_IF_PREDICATE_IS_FALSE ()
        {
            Assert.That(() => Ensure.ValidOperation(false, "test message"), Throws.InvalidOperationException.With.Message.EqualTo("test message"));
        }

        [Test]
        public void IT_SHOULD_DO_NOTHING_IF_PREDICATE_IS_TRUE ()
        {
            Assert.That(() => Ensure.ValidOperation(true, "test message"), Throws.Nothing);
        }
    }
}


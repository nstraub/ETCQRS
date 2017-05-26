using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Util;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GreaterThanOrEqualMutatorSpec
{
    [TestFixture]
    public class Property_NextMutator__Getter
    {
        [Test]
        public void IT_SHOULD_RETURN_A_NULL_MUTATOR ()
        {
            Assert.IsInstanceOf<NullMutator>(new GreaterThanOrEqualMutator(new MutatorFlyweightFactory()).NextMutator);
        }
    }
}


using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Factories;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Property_NextMutator__Getter
    {
        [Test]
        public void IT_SHOULD_RETURN_A_GENERIC_MUTATOR()
        {
            Assert.IsInstanceOf<NullMutator>(new LessThanOrEqualMutator(new MutatorFlyweightFactory()).NextMutator);
        }
    }
}


using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutator;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Property_NextMutator__Getter : TestsFor<LessThanOrEqualMutator>
    {
        [Test]
        public void IT_SHOULD_RETURN_A_GENERIC_MUTATOR()
        {
            GetMock<IFlyweightFactory<IExpressionMutator>>().Setup(f => f.Get("Null")).Returns(new NullMutator());
            Assert.That(Subject.NextMutator, Is.InstanceOf<NullMutator>());
        }
    }
}


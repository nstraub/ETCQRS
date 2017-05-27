using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutator;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GreaterThanOrEqualMutatorSpec
{
    [TestFixture]
    public class Property_NextMutator__Getter : TestsFor<GreaterThanOrEqualMutator>
    {
        [Test]
        public void IT_SHOULD_RETURN_A_NULL_MUTATOR ()
        {
            GetMock<IFlyweightFactory<IExpressionMutator>>().Setup(f => f.Get("Null")).Returns(new NullMutator());
            Assert.That(Subject.NextMutator, Is.InstanceOf<NullMutator>());
        }
    }
}


using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.ThrowingMutatorSpec

{
    [TestFixture]
    public class Method_Execute : TestsFor<ThrowingMutator>
    {
        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXPRESSION ()
        {
            Assert.That(() => Subject.Execute(GetMock<IQueryDescriptor>().Object), Throws.InvalidOperationException);
        }
    }
}


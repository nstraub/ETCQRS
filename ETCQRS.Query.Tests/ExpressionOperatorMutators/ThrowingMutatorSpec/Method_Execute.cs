using System;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutators;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.ThrowingMutatorSpec

{
    [TestFixture]
    public class Method_Execute
    {
        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXPRESSION ()
        {
            Assert.Throws<InvalidOperationException>(() => new ThrowingMutator().Execute(new Mock<IQueryDescriptor>().Object));
        }
    }
}


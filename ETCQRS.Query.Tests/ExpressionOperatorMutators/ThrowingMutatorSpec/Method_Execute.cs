using System;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.ThrowingMutatorSpec

{
    [TestFixture]
    public class Method_Execute : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXPRESSION ()
        {
            Assert.Throws<InvalidOperationException>(() => Kernel.Get<ThrowingMutator>().Execute(Kernel.Get<IQueryDescriptor>()));
        }
    }
}


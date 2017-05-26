using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.LessThanOrEqualMutatorSpec

{
    [TestFixture]
    public class Property_NextMutator__Getter : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_RETURN_A_GENERIC_MUTATOR()
        {
            Assert.IsInstanceOf<NullMutator>(Kernel.Get<LessThanOrEqualMutator>().NextMutator);
        }
    }
}


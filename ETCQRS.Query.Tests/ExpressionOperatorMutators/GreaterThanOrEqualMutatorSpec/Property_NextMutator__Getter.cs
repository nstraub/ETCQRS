using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GreaterThanOrEqualMutatorSpec
{
    [TestFixture]
    public class Property_NextMutator__Getter : NinjectFixture
    {
        [Test]
        public void IT_SHOULD_RETURN_A_NULL_MUTATOR ()
        {
            Assert.IsInstanceOf<NullMutator>(Kernel.Get<GreaterThanOrEqualMutator>().NextMutator);
        }
    }
}


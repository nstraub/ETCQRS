using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Moq;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;

namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.MutatorObserverSpec
{
    [TestFixture]
    public class Method_Update : NinjectFixture
    {
        private Mock<IQueryDescriptor> _descriptorMock;
        private IObserver _observer;

        [OneTimeSetUp]
        public void FixtureSetup ()
        {
            Kernel.Load(new BinaryExpressionsGraph(Expression.Constant(3)));
        }

        [SetUp]
        public void TestSetup ()
        {
            _descriptorMock = Kernel.GetMock<IQueryDescriptor>();
            
            _observer = Kernel.Get<MutatorObserver>();
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_GREATER_THAN_OR_EQUAL_IF_PASSED_EXPRESSION_IS_OF_TYPE_GREATER_THAN ()
        {
            _observer.Update(GetQuery("GreaterThan"));
            _descriptorMock.Verify(d => d.SetMutator(It.IsAny<GreaterThanOrEqualMutator>()), Times.Once);
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_LESS_THAN_OR_EQUAL_IF_PASSED_EXPRESSION_IS_OF_TYPE_LESS_THAN ()
        {
            _observer.Update(GetQuery("LessThan"));
            _descriptorMock.Verify(d => d.SetMutator(It.IsAny<LessThanOrEqualMutator>()), Times.Once);
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_NULL_IF_PASSED_EXPRESSION_IS_OF_ANY_OTHER_TYPE ()
        {
            _observer.Update(GetQuery("Equal"));
            _descriptorMock.Verify(d => d.SetMutator(It.IsAny<NullMutator>()), Times.Once);
        }
    }
}


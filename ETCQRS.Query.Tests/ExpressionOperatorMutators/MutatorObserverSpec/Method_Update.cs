using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Util;

using Moq;

using NUnit.Framework;

/**
 * it should
 *      change mutator to greater than or equal if passed expression is of type greater than
 *      change mutator to less than or equal if passed expression is of type less than
 *      change mutator to null if passed expression is of any other type
 **/
namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.MutatorObserverSpec
{
    [TestFixture]
    public class Method_Update
    {
        private Mock<IQueryDescriptor> _descriptorMock;
        private IObserver _observer;
        private MutatorFlyweightFactory _mutators = new MutatorFlyweightFactory ();

        [SetUp]
        public void SetupTest ()
        {
            _descriptorMock = new Mock<IQueryDescriptor>(MockBehavior.Strict);
            
            _observer = new MutatorObserver(_descriptorMock.Object, _mutators);
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_GREATER_THAN_OR_EQUAL_IF_PASSED_EXPRESSION_IS_OF_TYPE_GREATER_THAN ()
        {
            _descriptorMock.Setup(d => d.SetMutator(It.IsAny<GreaterThanOrEqualMutator>()));
            _observer.Update(Expression.GreaterThan(Expression.Constant(3), Expression.Constant(3)));
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_LESS_THAN_OR_EQUAL_IF_PASSED_EXPRESSION_IS_OF_TYPE_LESS_THAN ()
        {
            _descriptorMock.Setup(d => d.SetMutator(It.IsAny<LessThanOrEqualMutator>()));
            _observer.Update(Expression.LessThan(Expression.Constant(3), Expression.Constant(3)));
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_NULL_IF_PASSED_EXPRESSION_IS_OF_ANY_OTHER_TYPE ()
        {
            _descriptorMock.Setup(d => d.SetMutator(It.IsAny<NullMutator>()));
            _observer.Update(Expression.Equal(Expression.Constant(3), Expression.Constant(3)));
        }
    }
}


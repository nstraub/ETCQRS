using System.Collections.Generic;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;

using Moq;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.MutatorObserverSpec
{
    [TestFixture]
    public class Method_Update : TestsFor<MutatorObserver>
    {
        private Mock<IQueryDescriptor> _descriptorMock;
        private IDictionary<string, BinaryExpression> _expressions;

        private BinaryExpression GetQuery (string key)
        {
            return _expressions[key];
        }

        [SetUp]
        public void TestSetup ()
        {
            _descriptorMock = GetMock<IQueryDescriptor>();

            var constant = Expression.Constant(3);
            _expressions = new Dictionary<string, BinaryExpression>
            {
                { "Equal", Expression.Equal(constant, constant) },
                { "GreaterThan", Expression.GreaterThan(constant, constant) },
                { "LessThan", Expression.LessThan(constant, constant) }
            };
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_GREATER_THAN_OR_EQUAL_IF_PASSED_EXPRESSION_IS_OF_TYPE_GREATER_THAN ()
        {
            Subject.Update(GetQuery("GreaterThan"));
            _descriptorMock.Verify(d => d.SetMutator(It.IsAny<GreaterThanOrEqualMutator>()), Times.Once);
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_LESS_THAN_OR_EQUAL_IF_PASSED_EXPRESSION_IS_OF_TYPE_LESS_THAN ()
        {
            Subject.Update(GetQuery("LessThan"));
            _descriptorMock.Verify(d => d.SetMutator(It.IsAny<LessThanOrEqualMutator>()), Times.Once);
        }

        [Test]
        public void IT_SHOULD_CHANGE_MUTATOR_TO_NULL_IF_PASSED_EXPRESSION_IS_OF_ANY_OTHER_TYPE ()
        {
            Subject.Update(GetQuery("Equal"));
            _descriptorMock.Verify(d => d.SetMutator(It.IsAny<NullMutator>()), Times.Once);
        }
    }
}


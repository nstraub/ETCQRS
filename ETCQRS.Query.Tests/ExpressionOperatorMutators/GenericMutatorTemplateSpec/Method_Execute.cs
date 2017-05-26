using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutator;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GenericMutatorTemplateSpec
{
    /**
     * 
     * The Generic Mutator Template is the Template Method for all mutators with normal (i.e. simple mutation) functionality.
     * it should:
     *      - Mutate the operator for the latest expression added.
     *      - Skip mutation if TargetExpression is Null.
     *      - Skip mutation if TargetExpression is equal to current expression TODO: no added value for now
     *      - Be able to handle a query with multiple expressions.
     *      - Keep The Conditional Logic Operator when handling multiple expressions
     *      - Throw an InvalidOperationException if there is no query present.
     *      - Set the next mutator on the context.
     *      - Throw an error if there is no mutator to set next
     * 
     */
    [TestFixture]
    public class Method_Execute
    {
        private Mock<GenericMutatorTemplate> _mutatorMock;
        private Mock<IQueryDescriptor> _contextMock;
        private BinaryExpression _initialQuery;
        private Mock<IExpressionMutator> _nextMutatorMock;
        private GenericMutatorTemplate _mutator;
        private IQueryDescriptor _context;

        [SetUp]
        public void TestSetup ()
        {
            _mutatorMock = new Mock<GenericMutatorTemplate>
                           {
                               CallBase = true
                           };

            _nextMutatorMock = new Mock<IExpressionMutator>();
            _mutatorMock.SetupGet(m => m.NextMutator).Returns(_nextMutatorMock.Object);
            _mutatorMock.Setup(m => m.GetTargetExpression()).Returns(Expression.NotEqual);

            _contextMock = new Mock<IQueryDescriptor>();
            _contextMock.SetupProperty(c => c.Query);
            _initialQuery = Expression.Equal(Expression.Constant(3), Expression.Constant(3));

            _mutator = _mutatorMock.Object;
            _context = _contextMock.Object;
            _context.Query = _initialQuery;


        }
        [Test]
        public void IT_SHOULD_MUTATE_THE_OPERATOR_OF_LATEST_EXPRESSION ()
        {
            _mutator.Execute(_context);

            Assert.AreEqual(ExpressionType.NotEqual, _context.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_SKIP_MUTATION_IF_TARGET_EXPRESSION_IS_NULL ()
        {
            _mutatorMock.Setup(m => m.GetTargetExpression()).Returns(() => null);
            

            
            _mutator.Execute(_context);
            Assert.AreSame(_initialQuery, _context.Query);
        }

        [Test]
        public void IT_SHOULD_BE_ABLE_TO_HANDLE_A_QUERY_WITH_MULTIPLE_EXPRESSIONS ()
        {
            _context.Query = Expression.AndAlso(_initialQuery, _initialQuery);

            _mutator.Execute(_context);

            Assert.AreEqual(ExpressionType.Equal, _context.Query.Left.NodeType);
            Assert.AreEqual(ExpressionType.NotEqual, _context.Query.Right.NodeType);
        }

        [Test]
        public void IT_SHOULD_KEEP_THE_CONDITIONAL_LOGIC_OPERATOR_WHEN_HANDLING_AND_ALSO_EXPRESSIONS ()
        {
            _context.Query = Expression.AndAlso(_initialQuery, _initialQuery);

            _mutator.Execute(_context);

            Assert.AreEqual(ExpressionType.AndAlso, _context.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_KEEP_THE_CONDITIONAL_LOGIC_OPERATOR_WHEN_HANDLING_OR_ELSE_EXPRESSIONS ()
        {
            _context.Query = Expression.OrElse(_initialQuery, _initialQuery);

            _mutator.Execute(_context);

            Assert.AreEqual(ExpressionType.OrElse, _context.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXCEPTION_IF_THERE_IS_NO_QUERY_PRESENT ()
        {
            _context.Query = null;

            var exception = Assert.Throws<NullReferenceException>(() => _mutator.Execute(_context));
            Assert.AreEqual("There is no query to mutate", exception.Message);
        }

        [Test]
        public void IT_SHOULD_SET_THE_NEXT_MUTATOR_ON_THE_CONTEXT ()
        {

            _mutator.Execute(_context);

            _contextMock.Verify(c => c.SetMutator(_nextMutatorMock.Object), Times.Once);
        }

        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXPRESSION_IF_THERE_IS_NO_MUTATOR_TO_SET_NEXT ()
        {
            _mutatorMock.SetupGet(m => m.NextMutator).Returns(() => null);

            var exception = Assert.Throws<NullReferenceException>(() => _mutator.Execute(_context));
            Assert.AreEqual("There is no mutator to set next", exception.Message);
        }
    }
}


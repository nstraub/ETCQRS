using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutator;

using Moq;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GenericMutatorTemplateSpec
{

    [TestFixture]
    public class Method_Execute : TestsFor<GenericMutatorTemplate>
    {
        private IQueryDescriptor _context;
        private IDictionary<string, BinaryExpression> _expressions;

        [SetUp]
        public void TestSetup ()
        {
            var mutatorMock = GetMock<GenericMutatorTemplate>();
            mutatorMock.CallBase = true;
            mutatorMock.Setup(m => m.GetTargetExpression()).Returns(Expression.NotEqual);
            mutatorMock.SetupGet(m => m.NextMutator).Returns(GetMock<IExpressionMutator>().Object);

            var constant = Expression.Constant(3);
            var query = Expression.Equal(constant, constant);
            _expressions = new Dictionary<string, BinaryExpression>
                          {
                              { "Initial", query },
                              { "AndAlso", Expression.AndAlso(query, query) },
                              { "OrElse", Expression.OrElse(query, query) }
                          };

            GetMock<IQueryDescriptor>().SetupProperty(c => c.Query);

            _context = GetMock<IQueryDescriptor>().Object;
            _context.Query = GetQuery("Initial");
        }

        private BinaryExpression GetQuery (string key)
        {
            return _expressions[key];
        }

        private void Mutate ()
        {
            Subject.Execute(_context);
        }


        [Test]
        public void IT_SHOULD_MUTATE_THE_OPERATOR_OF_LATEST_EXPRESSION ()
        {
            Mutate();

            Assert.That(_context.Query.NodeType, Is.EqualTo(ExpressionType.NotEqual));
        }

        [Test]
        public void IT_SHOULD_SKIP_MUTATION_IF_TARGET_EXPRESSION_IS_NULL ()
        {
            GetMock<GenericMutatorTemplate>().Setup(m => m.GetTargetExpression()).Returns(() => null);
            
            Mutate();
            Assert.That(_context.Query, Is.SameAs(GetQuery("Initial")));
        }

        [Test]
        public void IT_SHOULD_BE_ABLE_TO_HANDLE_A_QUERY_WITH_MULTIPLE_EXPRESSIONS ()
        {
            _context.Query = GetQuery("AndAlso");

            Mutate();

            Assert.That(_context.Query.Left.NodeType, Is.EqualTo(ExpressionType.Equal));
            Assert.That(_context.Query.Right.NodeType, Is.EqualTo(ExpressionType.NotEqual));
        }

        [Test]
        public void IT_SHOULD_KEEP_THE_CONDITIONAL_LOGIC_OPERATOR_WHEN_HANDLING_AND_ALSO_EXPRESSIONS ()
        {
            _context.Query = GetQuery("AndAlso");

            Mutate();

            Assert.That(_context.Query.NodeType, Is.EqualTo(ExpressionType.AndAlso));
        }

        [Test]
        public void IT_SHOULD_KEEP_THE_CONDITIONAL_LOGIC_OPERATOR_WHEN_HANDLING_OR_ELSE_EXPRESSIONS ()
        {
            _context.Query = GetQuery("OrElse");

            Mutate();

            Assert.That(_context.Query.NodeType, Is.EqualTo(ExpressionType.OrElse));
        }

        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXCEPTION_IF_THERE_IS_NO_QUERY_PRESENT ()
        {
            _context.Query = null;

            Assert.That(Mutate, Throws.Exception.TypeOf<NullReferenceException>().With.Message.EqualTo("There is no query to mutate"));
        }

        [Test]
        public void IT_SHOULD_SET_THE_NEXT_MUTATOR_ON_THE_CONTEXT ()
        {
            Mutate();

            GetMock<IQueryDescriptor>().Verify(c => c.SetMutator(GetMock<IExpressionMutator>().Object), Times.Once());
        }

        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXPRESSION_IF_THERE_IS_NO_MUTATOR_TO_SET_NEXT ()
        {
            GetMock<GenericMutatorTemplate>().SetupGet(m => m.NextMutator).Returns(() => null);

            Assert.That(Mutate, Throws.Exception.TypeOf<NullReferenceException>().With.Message.EqualTo("There is no mutator to set next"));
        }
    }
}


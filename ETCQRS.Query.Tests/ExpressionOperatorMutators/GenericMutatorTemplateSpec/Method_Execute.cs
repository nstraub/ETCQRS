using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Moq;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;
using Ninject;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.GenericMutatorTemplateSpec
{

    [TestFixture]
    public class Method_Execute : NinjectFixture
    {
        private IQueryDescriptor _context;

        [OneTimeSetUp]
        public void FixtureSetup ()
        {
            
            Kernel.Load(new InitialQueryGraph(Expression.Equal(Expression.Constant(3), Expression.Constant(3))));
        }

        [SetUp]
        public void TestSetup ()
        {
            var mutatorMock = Kernel.GetMock<GenericMutatorTemplate>();
            mutatorMock.CallBase = true;
            mutatorMock.Setup(m => m.GetTargetExpression()).Returns(Expression.NotEqual);
            mutatorMock.SetupGet(m => m.NextMutator).Returns(Kernel.Get<IExpressionMutator>());

            Kernel.GetMock<IQueryDescriptor>().SetupProperty(c => c.Query);

            _context = Kernel.Get<IQueryDescriptor>();
            _context.Query = GetQuery("Initial");
        }
        
        private void Mutate ()
        {
            Kernel.Get<GenericMutatorTemplate>().Execute(_context);
        }


        [Test]
        public void IT_SHOULD_MUTATE_THE_OPERATOR_OF_LATEST_EXPRESSION ()
        {
            Mutate();

            Assert.AreEqual(ExpressionType.NotEqual, _context.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_SKIP_MUTATION_IF_TARGET_EXPRESSION_IS_NULL ()
        {
            Kernel.GetMock<GenericMutatorTemplate>().Setup(m => m.GetTargetExpression()).Returns(() => null);
            

            
            Mutate();
            Assert.AreSame(GetQuery("Initial"), _context.Query);
        }

        [Test]
        public void IT_SHOULD_BE_ABLE_TO_HANDLE_A_QUERY_WITH_MULTIPLE_EXPRESSIONS ()
        {
            _context.Query = GetQuery("AndAlso");

            Mutate();

            Assert.AreEqual(ExpressionType.Equal, _context.Query.Left.NodeType);
            Assert.AreEqual(ExpressionType.NotEqual, _context.Query.Right.NodeType);
        }

        [Test]
        public void IT_SHOULD_KEEP_THE_CONDITIONAL_LOGIC_OPERATOR_WHEN_HANDLING_AND_ALSO_EXPRESSIONS ()
        {
            _context.Query = GetQuery("AndAlso");

            Mutate();

            Assert.AreEqual(ExpressionType.AndAlso, _context.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_KEEP_THE_CONDITIONAL_LOGIC_OPERATOR_WHEN_HANDLING_OR_ELSE_EXPRESSIONS ()
        {
            _context.Query = GetQuery("OrElse");

            Mutate();

            Assert.AreEqual(ExpressionType.OrElse, _context.Query.NodeType);
        }

        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXCEPTION_IF_THERE_IS_NO_QUERY_PRESENT ()
        {
            _context.Query = null;

            var exception = Assert.Throws<NullReferenceException>(Mutate);
            Assert.AreEqual("There is no query to mutate", exception.Message);
        }

        [Test]
        public void IT_SHOULD_SET_THE_NEXT_MUTATOR_ON_THE_CONTEXT ()
        {
            Mutate();

            Kernel.GetMock<IQueryDescriptor>().Verify(c => c.SetMutator(Kernel.Get<IExpressionMutator>()), Times.Once);
        }

        [Test]
        public void IT_SHOULD_THROW_AN_INVALID_OPERATION_EXPRESSION_IF_THERE_IS_NO_MUTATOR_TO_SET_NEXT ()
        {
            Kernel.GetMock<GenericMutatorTemplate>().SetupGet(m => m.NextMutator).Returns(() => null);

            var exception = Assert.Throws<NullReferenceException>(Mutate);
            Assert.AreEqual("There is no mutator to set next", exception.Message);
        }
    }
}


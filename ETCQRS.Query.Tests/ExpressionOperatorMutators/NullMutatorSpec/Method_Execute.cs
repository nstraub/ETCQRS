using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Tests.SUTUtils;
using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.NullMutatorSpec

{
    [TestFixture]
    public class Method_Execute : NinjectFixture
    {
        [OneTimeSetUp]
        public void FixtureSetup ()
        {
            Kernel.Load(new BinaryExpressionsGraph(Expression.Constant(3)));
        }

        [Test]
        public void IT_SHOULD_NOT_PERFORM_ANY_OPERATION ()
        {
            var contextMock = Kernel.GetMock<IQueryDescriptor>();
            contextMock.SetupProperty(c => c.Query);

            var query = GetQuery("Equal");

            var context = contextMock.Object;
            context.Query = query;

            new NullMutator().Execute(context);

            Assert.AreSame(query, context.Query);
        }
    }
}


using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.NullMutatorSpec

{
    [TestFixture]
    public class Method_Execute
    {
        [Test]
        public void IT_SHOULD_NOT_PERFORM_ANY_OPERATION ()
        {
            var contextMock = new Mock<IQueryDescriptor>();
            contextMock.SetupProperty(c => c.Query);
            var query = Expression.Equal(Expression.Constant(3), Expression.Constant(3));
            contextMock.Object.Query = query;

            new NullMutator().Execute(contextMock.Object);

            Assert.AreSame(query, contextMock.Object.Query);
        }
    }
}


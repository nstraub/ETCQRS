using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.ExpressionOperatorMutators.NullMutatorSpec

{
    [TestFixture]
    public class Method_Execute : TestsFor<NullMutator>
    {
        [Test]
        public void IT_SHOULD_NOT_PERFORM_ANY_OPERATION ()
        {
            var contextMock = GetMock<IQueryDescriptor>();
            contextMock.SetupProperty(c => c.Query);

            var constant = Expression.Constant(3);
            var query = Expression.Equal(constant, constant);

            var context = contextMock.Object;
            context.Query = query;

            Subject.Execute(context);

            Assert.That(context.Query, Is.SameAs(query));
        }
    }
}


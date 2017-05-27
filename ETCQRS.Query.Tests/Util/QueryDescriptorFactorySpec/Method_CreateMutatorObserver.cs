using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Factories;

using Moq;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.Util.QueryDescriptorFactorySpec
{
    [TestFixture]
    public class Method_CreateMutatorObserver : TestsFor<QueryDescriptorFactory>
    {
        [Test]
        public void IT_SHOULD_CREATE_A_NEW_MUTATOR_OBSERVER_WITH_PROVIDED_DESCRIPTOR_AS_PARAMETER ()
        {
            GetMock<IQueryDescriptor>().Setup(qd => qd.SetMutator(It.IsAny<NullMutator>()));
            var mutatorObserver = Subject.CreateMutatorObserver(Get<IQueryDescriptor>());
            mutatorObserver.Update(Expression.Equal(Expression.Constant(3), Expression.Constant(3)));
            GetMock<IQueryDescriptor>().Verify();
        }
    }
}


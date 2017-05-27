using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Builder;

using NUnit.Framework;

using TestFramework.NUnit.Ninject.Moq;


namespace ETCQRS.Query.Tests.Builder.QueryExpressionBuilderSpec
{
    [TestFixture]
    public class Method_Mutate : TestsFor<QueryExpressionBuilder>
    {
        [Test]
        public void IT_SHOULD_CALL_MUTATE_ON_THE_DESCRIPTOR ()
        {
            var descriptorMock = GetMock<IQueryDescriptor>();
            descriptorMock.Setup(d => d.Mutate());
            Subject.Descriptor = descriptorMock.Object;
            Subject.Mutate();

            descriptorMock.Verify();
        }
    }
}


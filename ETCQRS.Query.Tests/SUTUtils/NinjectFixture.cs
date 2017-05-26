using System.Linq.Expressions;

using ETCQRS.Query.Tests.SUTUtils.NinjectModules;

using Ninject;
using Ninject.MockingKernel.Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.SUTUtils
{
    public class NinjectFixture
    {
        protected readonly MoqMockingKernel Kernel;

        public NinjectFixture ()
        {
            Kernel = new MoqMockingKernel();
            Kernel.Load(new BaseExpressionGraph(), new BaseObjectGraph());
        }


        [TearDown]
        public void TestTeardown ()
        {
            Kernel.Reset();
        }
        protected BinaryExpression GetQuery (string key)
        {
            return Kernel.Get<BinaryExpression>(key);
        }

        protected ParameterExpression GetParameter ()
        {
            return Kernel.Get<ParameterExpression>();
        }

        protected MemberExpression GetProperty ()
        {
            return Kernel.Get<MemberExpression>();
        }
    }
}

using System.Linq.Expressions;

using Ninject.Modules;


namespace ETCQRS.Query.Tests.SUTUtils.NinjectModules
{
    public class BaseExpressionGraph : NinjectModule
    {
        public override void Load ()
        {
            var parameter = Expression.Parameter(typeof(FakeEntity));
            Bind<ParameterExpression>().ToConstant(parameter);
            Bind<MemberExpression>().ToConstant(Expression.Property(parameter, "Test"));
        }
    }
}

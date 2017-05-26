using System.Linq.Expressions;

using Ninject.Modules;


namespace ETCQRS.Query.Tests.SUTUtils.NinjectModules
{
    public class InitialQueryGraph : NinjectModule
    {
        private readonly BinaryExpression _initialQuery;

        public InitialQueryGraph (BinaryExpression initialQuery)
        {
            _initialQuery = initialQuery;
        }

        public override void Load ()
        {
            Bind<BinaryExpression>().ToMethod(ctx => Expression.AndAlso(_initialQuery, _initialQuery)).Named("AndAlso");
            Bind<BinaryExpression>().ToMethod(ctx => Expression.OrElse(_initialQuery, _initialQuery)).Named("OrElse");
            Bind<BinaryExpression>().ToConstant(_initialQuery).Named("Initial");
        }
    }
}

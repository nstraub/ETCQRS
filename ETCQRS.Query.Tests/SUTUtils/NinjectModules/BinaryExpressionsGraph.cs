using System.Linq.Expressions;

using Ninject.Modules;


namespace ETCQRS.Query.Tests.SUTUtils.NinjectModules
{
    public class BinaryExpressionsGraph : NinjectModule
    {
        private readonly ConstantExpression _value;

        public BinaryExpressionsGraph (ConstantExpression value)
        {
            _value = value;
        }
        public override void Load ()
        {
            Bind<BinaryExpression>().ToConstant(Expression.Equal(_value, _value)).Named("Equal");
            Bind<BinaryExpression>().ToConstant(Expression.GreaterThan(_value, _value)).Named("GreaterThan");
            Bind<BinaryExpression>().ToConstant(Expression.LessThan(_value, _value)).Named("LessThan");
        }
    }
}

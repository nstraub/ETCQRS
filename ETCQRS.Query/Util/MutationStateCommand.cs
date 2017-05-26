using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Util
{
    public class MutationStateCommand
    {
        private readonly IFlyweightFactory<IExpressionMutator> _mutators;
        private readonly IQueryDescriptor _descriptor;

        public MutationStateCommand(IFlyweightFactory<IExpressionMutator> mutators, IQueryDescriptor descriptor)
        {
            _mutators = mutators;
            _descriptor = descriptor;
        }
        public void Execute (ExpressionType expressionType)
        {
            if (expressionType == ExpressionType.GreaterThan)
            {
                _descriptor.SetMutator(_mutators.Get("GreaterThan"));
            }
            else if (expressionType == ExpressionType.LessThan)
            {
                _descriptor.SetMutator(_mutators.Get("LessThan"));
            }
            else
            {
                _descriptor.SetMutator(_mutators.Get("Throwing"));
            }
        }
    }
}

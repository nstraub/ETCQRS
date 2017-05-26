using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.ExpressionOperatorMutators
{
    public class NullMutator : IExpressionMutator
    {
        #region Implementation of IExpressionMutator

        public void Execute (IQueryDescriptor context)
        { }

        #endregion
    }
}

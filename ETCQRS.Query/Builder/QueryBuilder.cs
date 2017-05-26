using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Resources;
using ETCQRS.Query.Util;


namespace ETCQRS.Query.Builder
{
    public class QueryBuilder : IQueryBuilder
    {
        private Func<Expression, Expression, BinaryExpression> _queryLinker = Expression.AndAlso;
        private IList<IObserver> _observers = new List<IObserver>();

        public virtual void AddExpression (IQueryDescriptor descriptor, Func<Expression, Expression, BinaryExpression> operatorFunc, object value)
        {
            Ensure.NullReference(descriptor.PropertyExpression, ErrorMessages.PropertyNullReference);

            var query = operatorFunc(descriptor.PropertyExpression, Expression.Constant(value));

            if (descriptor.Query is null)
            {
                descriptor.Query = query;
            }
            else
            {
                descriptor.Query = QueryLinker(descriptor.Query, query);
            }
            foreach (var observer in _observers)
            {
                observer.Update(query);
            }
        }

        public Func<Expression, Expression, BinaryExpression> QueryLinker
        {
            private get { return _queryLinker; }
            set
            {
                Ensure.ValidOperation(value == Expression.AndAlso || value == Expression.OrElse, ErrorMessages.InvalidQueryLinkerExpression);
                _queryLinker = value;
            }
        }
        
        public IQueryBuilder SetMutator (IQueryDescriptor descriptor, IExpressionMutator mutator)
        {
            descriptor.SetMutator(mutator);
            return this;
        }

        public IQueryBuilder Mutate (IQueryDescriptor descriptor)
        {
            descriptor.Mutate();
            return this;
        }



        #region Implementation of IObservable

        public void Subscribe (params IObserver[] observers)
        {
            _observers = observers;
        }

        #endregion
    }
}

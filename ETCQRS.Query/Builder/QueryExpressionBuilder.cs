using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using EnsureThat;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Resources;


namespace ETCQRS.Query.Builder
{
    public class QueryExpressionBuilder : IQueryExpressionBuilder
    {
        private Func<Expression, Expression, BinaryExpression> _queryLinker = Expression.AndAlso;
        private IList<IObserver> _observers = new List<IObserver>();

        public IQueryExpressionBuilder And
        {
            get
            {
                QueryLinker = Expression.AndAlso;
                return this;
            }
        }

        public IQueryExpressionBuilder Or
        {
            get
            {
                QueryLinker = Expression.OrElse;
                return this;
            }
        }

        public IQueryDescriptor Descriptor { private get; set; }

        public virtual IQueryExpressionBuilder AddExpression (Func<Expression, Expression, BinaryExpression> operatorFunc, object value)
        {
            Ensure.That(Descriptor.Property).WithException((e) => new NullReferenceException(ErrorMessages.PropertyNullReference)).IsNotNull();

            var query = operatorFunc(Descriptor.Property, Expression.Constant(value));

            if (Descriptor.Query is null)
            {
                Descriptor.Query = query;
            }
            else
            {
                Descriptor.Query = QueryLinker(Descriptor.Query, query);
            }
            foreach (var observer in _observers)
            {
                observer.Update(query);
            }

            return this;
        }

        public IQueryExpressionBuilder Mutate ()
        {
            Descriptor.Mutate();
            return this;
        }

        public Func<Expression, Expression, BinaryExpression> QueryLinker
        {
            get { return _queryLinker; }
            set
            {
                Ensure.That(value == Expression.AndAlso || value == Expression.OrElse).WithException(e => new InvalidOperationException(ErrorMessages.InvalidQueryLinkerExpression)).IsTrue();
                _queryLinker = value;
            }
        }


        public void Subscribe (params IObserver[] observers)
        {
            _observers = observers;
        }
    }
}

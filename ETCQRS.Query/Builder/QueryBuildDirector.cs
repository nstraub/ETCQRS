using System;

using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Resources;
using ETCQRS.Query.Util;


namespace ETCQRS.Query.Builder
{
    public class QueryBuildDirector : IQueryBuildDirector
    {
        private readonly IQueryDescriptorFactory _descriptorFactory;
        private readonly IQueryBuilder _builder;
        private IQueryDescriptor _descriptor;
        private Type _currentType;

        public QueryBuildDirector (IQueryDescriptorFactory descriptorFactory, IQueryBuilder builder)
        {
            _descriptorFactory = descriptorFactory;
            _builder = builder;
        }

        public IQueryBuildDirector UsingType (Type type)
        {
            Ensure.ValidOperation(_currentType == type || _descriptor is null, ErrorMessages.AlreadyBuildingQuery);
            if (_descriptor is null)
            {
                _descriptor = _descriptorFactory.Create(type);
                _currentType = type;
                _builder.Subscribe(_descriptorFactory.CreateMutatorObserver(_descriptor));
            }
            return this;
        }

        public IQueryBuildDirector Property (string propertyName)
        {
            _descriptor.PropertyExpression = Expression.Property(_descriptor.Parameter, propertyName);
            return this;
        }





        #region IQueryBuilder Facade

        public virtual IQueryBuildDirector AddExpression (Func<Expression, Expression, BinaryExpression> operatorFunc, object value)
        {
            _builder.AddExpression(_descriptor, operatorFunc, value);
            return this;
        }

        public IQueryBuildDirector And
        {
            get
            {
                _builder.QueryLinker = Expression.AndAlso;
                return this;
            }
        }
        public IQueryBuildDirector Or
        {
            get
            {
                _builder.QueryLinker = Expression.OrElse;
                return this;
            }
        }

        public IQueryBuildDirector Mutate ()
        {
            return InnerMutate();
        }

        private IQueryBuildDirector InnerMutate ()
        {
//TODO            Contract.Requires<InvalidOperationException>(_descriptor.Mutator is GreaterThanMutator || _descriptor.Mutator is LessThanMutator);
            _descriptor.Mutate();
            return this;
        }

        #endregion






        public Expression<Func<T, bool>> GetResult<T> () where T : class
        {
            var expression = (Expression<Func<T, bool>>) Expression.Lambda(_descriptor.Query, _descriptor.Parameter);
            _descriptor = null;
            return expression;
        }

    }
}

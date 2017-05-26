using System;

using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Resources;
using ETCQRS.Query.Util;


namespace ETCQRS.Query.Builder
{
    public class QueryBuildFacade : IQueryBuildFacade
    {
        public IQueryComposite Composite { get; }
        private readonly IQueryDescriptorFactory _descriptorFactory;
        private readonly IQueryExpressionBuilder _expressionBuilder;
        private IQueryDescriptor _descriptor;
        private Type _currentType;

        public QueryBuildFacade (IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder, IQueryComposite composite)
        {
            Composite = composite;
            _descriptorFactory = descriptorFactory;
            _expressionBuilder = expressionBuilder;
        }

        public IQueryBuildFacade UsingType (Type type)
        {
            Ensure.ValidOperation(_currentType == type || _descriptor is null, ErrorMessages.AlreadyBuildingQuery);
            if (_descriptor is null)
            {
                _descriptor = _descriptorFactory.Create(type);
                _currentType = type;
                _expressionBuilder.Subscribe(_descriptorFactory.CreateMutatorObserver(_descriptor));
            }
            return this;
        }

        public IQueryBuildFacade Property (string propertyName)
        {
            _descriptor.PropertyExpression = Expression.Property(_descriptor.Parameter, propertyName);
            return this;
        }


        public virtual IQueryBuildFacade AddExpression (Func<Expression, Expression, BinaryExpression> operatorFunc, object value)
        {
            _expressionBuilder.AddExpression(_descriptor, operatorFunc, value);
            return this;
        }

        public IQueryBuildFacade And
        {
            get
            {
                _expressionBuilder.QueryLinker = Expression.AndAlso;
                return this;
            }
        }
        public IQueryBuildFacade Or
        {
            get
            {
                _expressionBuilder.QueryLinker = Expression.OrElse;
                return this;
            }
        }

        public IQueryBuildFacade Mutate ()
        {
            _descriptor.Mutate();
            return this;
        }
        
        public Expression<Func<T, bool>> GetResult<T> () where T : class
        {
            var expression = (Expression<Func<T, bool>>) Expression.Lambda(_descriptor.Query, _descriptor.Parameter);
            _descriptor = null;
            return expression;
        }

    }
}

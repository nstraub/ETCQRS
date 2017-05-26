using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Builder;


namespace ETCQRS.Query.Abstractions.Builder {
    // ReSharper disable once UnusedTypeParameter
    public abstract class QueryBuilderTemplate<T> : IQueryBuilder where T : class, IQuery
    {
        protected readonly IQueryExpressionBuilder ExpressionBuilder;
        protected IQueryDescriptorFactory DescriptorFactory { get; }

        protected IQueryDescriptor Descriptor;

        protected string PropertyName;

        protected readonly CallFactory CallFactory = new CallFactory();
        protected readonly List<MethodCallExpression> MethodCall = new List<MethodCallExpression>();
        protected Type ParameterType { get; set; }

        protected QueryBuilderTemplate (IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder)
        {
            ExpressionBuilder = expressionBuilder;
            DescriptorFactory = descriptorFactory;
        }

        public virtual void InitProperties (IQuery query)
        {
            ParameterType = query.ParameterType;
            PropertyName = query.PropertyName;
        }

        public abstract void Init (IQuery query);

        public virtual void BuildParameter ()
        {
            Descriptor = DescriptorFactory.Create(ParameterType);
            ExpressionBuilder.Descriptor = Descriptor;
        }
        
        public virtual void BuildProperty ()
        {
            Descriptor.PropertyExpression = Expression.Property(Descriptor.Parameter, PropertyName);
        }
        public abstract void BuildExpression ();
        public abstract void BuildMethodCall ();
        public virtual MethodCallExpression[] GetResults ()
        {
            return MethodCall.ToArray();
        }
    }
}
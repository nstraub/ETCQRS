using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Abstractions.Builder {
    public abstract class QuerylessBuilder<T> : QueryBuilder<T> where T : class, IQuery {
        protected QuerylessBuilder (IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder) : base(descriptorFactory, expressionBuilder) { }
        
        public override void Init (IQuery query) { }

        public override void BuildParameter () { }

        public override void BuildProperty () { }
        public override void BuildExpression () { }
    }
}
using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Abstractions.Builder {
    public abstract class QuerylessBuilderTemplate<T> : QueryBuilderTemplate<T> where T : class, IQuery {
        protected QuerylessBuilderTemplate (IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder) : base(descriptorFactory, expressionBuilder) { }

        public override void InitProperties(IQuery query) { }
        public override void Init (IQuery query) { }

        public override void BuildParameter () { }

        public override void BuildProperty () { }
        public override void BuildExpression () { }
    }
}
using ETCQRS.Query.Abstractions.Base;
using System.Linq.Expressions;

namespace ETCQRS.Query.Abstractions.Builder
{
	public interface IQueryBuilder
	{
		void Init(IQuery query);

		void BuildParameter();

		void BuildProperty();

		void BuildExpression();

		void BuildMethodCalls();

		(string, LambdaExpression)[] GetResults();
	}
}

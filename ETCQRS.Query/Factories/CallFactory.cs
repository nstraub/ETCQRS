using AutoMapper;
using EnsureThat;
using ETCQRS.Query.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETCQRS.Query.Factories
{
	public class CallFactory
	{
		private readonly IConfigurationProvider _mapperConfigurationProvider;

		public IDictionary<string, Func<IQueryable, LambdaExpression, IQueryable>> Create { get; }

		public CallFactory(IConfigurationProvider mapperConfigurationProvider)
		{
			_mapperConfigurationProvider = mapperConfigurationProvider;
			Create = new Dictionary<string, Func<IQueryable, LambdaExpression, IQueryable>>
			{
				{ "ProjectTo", (source, lambda) => CreateProjectTo(source, (Expression<Func<Type>>) lambda) },
				{ "Where", CreateWhere },
				{ "Select", CreateSelect },
				{ "SelectMany", CreateSelectMany },
				{ "OfType", (source, lambda) => CreateOfType(source, (Expression<Func<Type>>) lambda) }
			};
		}

		private IQueryable CreateWhere(IQueryable source, LambdaExpression iterator)
		{
			var methodCall = Expression.Call(MethodInfos.Where.MakeGenericMethod(source.ElementType), new[] { source.Expression, Expression.Quote(iterator) });
			return source.Provider.CreateQuery(methodCall);
		}

		private IQueryable CreateSelect(IQueryable source, LambdaExpression iterator)
		{
			var methodCall = Expression.Call(MethodInfos.Select.MakeGenericMethod(source.ElementType, iterator.ReturnType),
				new[] { source.Expression, Expression.Quote(iterator) });
			return source.Provider.CreateQuery(methodCall);
		}

		private IQueryable CreateProjectTo(IQueryable source, Expression<Func<Type>> lambda)
		{
			var outType = lambda.Compile().Invoke();

			var genericFuncType = typeof(Func<,>).MakeGenericType(outType, typeof(object));
			var genericExpressionType = typeof(Expression<>).MakeGenericType(genericFuncType);

			var customMappings = genericExpressionType.MakeArrayType().GetConstructor(new[] { typeof(int) })
				?.Invoke(new Object[] { 0 });
			return (IQueryable)MethodInfos.Project.MakeGenericMethod(outType)
				.Invoke(null, new[] { source, _mapperConfigurationProvider, customMappings });
		}

		private IQueryable CreateSelectMany(IQueryable source, LambdaExpression iterator)
		{
			var methodCall = Expression.Call(MethodInfos.SelectMany.MakeGenericMethod(source.ElementType, iterator.ReturnType.GenericTypeArguments[0]), source.Expression, Expression.Quote(iterator));
			return source.Provider.CreateQuery(methodCall);
		}

		private IQueryable CreateOfType(IQueryable source, Expression<Func<Type>> lambda)
		{
			var outType = lambda.Compile().Invoke();
			Ensure.That(source.ElementType.IsAssignableFrom(outType)).WithException(e => new InvalidOperationException(ErrorMessages.WrongOfTypeCast));
			var methodCall = Expression.Call(MethodInfos.OfType.MakeGenericMethod(outType), Expression.Parameter(typeof(IQueryable<>).MakeGenericType(source.ElementType)));
			return source.Provider.CreateQuery(methodCall);
		}
	}
}

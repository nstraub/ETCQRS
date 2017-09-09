using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

using EnsureThat;

using ETCQRS.Query.Resources;

namespace ETCQRS.Query.Factories
{
	public class CallFactory
	{
		private readonly IConfigurationProvider _mapperConfigurationProvider;

		public CallFactory(IConfigurationProvider mapperConfigurationProvider)
		{
			_mapperConfigurationProvider = mapperConfigurationProvider;
		}

		public MethodCallExpression CreateWhere<T>(Expression<Func<T, bool>> iterator) where T : class
		{
			return Expression.Call(MethodInfos.Where.MakeGenericMethod(typeof(T)), Expression.Parameter(typeof(IQueryable<T>)), iterator);
		}

		public MethodCallExpression CreateSingle<T>(Expression<Func<T, bool>> iterator) where T : class
		{
			return Expression.Call(MethodInfos.Single.MakeGenericMethod(typeof(T)), Expression.Parameter(typeof(IQueryable<T>)), iterator);
		}

		public MethodCallExpression CreateSelect<TIn, TOut>(Expression<Func<TIn, TOut>> iterator) where TIn : class where TOut : class
		{
			return Expression.Call(MethodInfos.Select.MakeGenericMethod(typeof(TIn), typeof(TOut)), Expression.Parameter(typeof(IQueryable<TIn>)), iterator);
		}

		public MethodCallExpression CreateProjectTo<TIn, TOut>(params Expression<Func<TOut, object>>[] iterators)
		{
			return Expression.Call(MethodInfos.Project.MakeGenericMethod(typeof(TOut)), Expression.Parameter(typeof(IQueryable<TIn>)),
				Expression.Constant(_mapperConfigurationProvider),
				Expression.NewArrayInit(typeof(Expression<Func<TOut, object>>), iterators));
		}

		public MethodCallExpression CreateSelectMany<TIn, TOut>(Expression<Func<TIn, TOut>> iterator) where TIn : class where TOut : class
		{
			return Expression.Call(MethodInfos.SelectMany.MakeGenericMethod(typeof(TIn), typeof(TOut).GenericTypeArguments[0]), Expression.Parameter(typeof(IQueryable<TIn>)), iterator);
		}

		public MethodCallExpression CreateOfType<TIn, TOut>() where TIn : class where TOut : class
		{
			Ensure.That(typeof(TIn).IsAssignableFrom(typeof(TOut))).WithException(e => new InvalidOperationException(ErrorMessages.WrongOfTypeCast));
			return Expression.Call(MethodInfos.OfType.MakeGenericMethod(typeof(TOut)), Expression.Parameter(typeof(IQueryable<>).MakeGenericType(typeof(TIn))));
		}
	}
}

﻿using System.Linq;

using ETCQRS.Query.Abstractions.Base;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryComposer
    {
        void AddQuery(IQuery query, IQueryBuilder queryBuilder);

        IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source) where TIn : class where TOut : class;
    }
}
﻿using BITC.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BITC.Library.Pattern
{
    public interface IRepository<TEntity> where TEntity : IObjectState
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryFluent<TEntity> Query();
        //IQueryable Queryable(ODataQueryOptions<TEntity> oDataQueryOptions);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> query);
        IRepository<T> GetRepository<T>() where T : IObjectState;
    }
}

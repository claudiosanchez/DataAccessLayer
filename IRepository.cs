using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Framework
{
    public interface IRepository<TEntity> where TEntity : class

    {
        TEntity GetByID(object id);
        void Insert(TEntity entity);

        void Delete(object id);
        void Update(TEntity entity);

        void Save();
        // Super Get
        IEnumerable<TResult> Get<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null);
    }
}
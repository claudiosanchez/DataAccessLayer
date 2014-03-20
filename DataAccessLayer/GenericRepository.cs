using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Framework
{
    public class GenericRepository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected internal DbContext _context;
        protected internal readonly DbSet<TEntity> _dbSet;
        protected bool IsInitialized;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
           
        }


        public void Delete(object id)
        {
            _dbSet.Remove(GetByID(id));
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        /// <summary>
        ///     Retrieves a list of TEntities, filtered, sorted and projected to TResult.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="transform">The transform.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        public virtual IEnumerable<TResult> Get<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null)
        {
            // _dbSet is DbSet<TEntity> property of a DbContext
            var query = (filter == null) ? _dbSet : _dbSet.Where(filter);

            var notSortedResults = transform(query);

            return orderBy == null ? (IEnumerable<TResult>) notSortedResults : orderBy(notSortedResults).ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
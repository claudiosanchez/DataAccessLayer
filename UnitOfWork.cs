using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace DataAccess.Framework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory _factory;
        private readonly DbContext _context;

        public UnitOfWork(IRepositoryFactory factory, DbContext dbContext)
        {
            _factory = factory;
            _context = dbContext;
        }

        public IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class
        {
            return _factory.BuildRepository<TEntity>();
        }
		
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }

            catch (DbEntityValidationException entityValidation)
            {
                foreach (var prop  in entityValidation.EntityValidationErrors)
                {
                    Debug.WriteLine(prop);
                }
            }
        }
    }
}
namespace DataAccess.Framework
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class;
        void Save();
    }
}
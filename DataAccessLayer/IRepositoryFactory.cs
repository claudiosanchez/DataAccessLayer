namespace DataAccess.Framework
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> BuildRepository<TEntity>() where TEntity : class;
    }
}
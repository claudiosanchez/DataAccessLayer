using System;
using StructureMap;

namespace DataAccess.Framework
{
    public class RepositoryFactory:IRepositoryFactory
    {
        private readonly IContainer _container;

        public RepositoryFactory(IContainer container)
        {
            _container = container;
        }

        public IRepository<TEntity> BuildRepository<TEntity>()
            where TEntity : class
        {
            var candidate = _container.TryGetInstance<IRepository<TEntity>>();

            if (candidate == null)
                throw new NullReferenceException("The requested Repository Type was not registered with the container.");

            return candidate;
        }
    }
}
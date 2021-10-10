using Deployer.DatabaseModel;
using Deployer.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseDomainEntity
    {
        protected DeployerContext _deployerContext;
        private DbSet<T> _entities;
        public RepositoryBase(DeployerContext deployerContext)
        {
            _deployerContext = deployerContext;
            _entities = _deployerContext.Set<T>();
        }

        public virtual void SaveChanges()
        {
            _deployerContext.SaveChanges();
        }

        public virtual void Add(T step)
        {
            _deployerContext.Add(step);
        }

        public virtual void Remove(int id)
        {
            var toRemove = _entities
                .SingleOrDefault(x => id == x.Id);

            _deployerContext.Remove(toRemove);
        }

        public virtual void Remove(List<int> ids)
        {
            var toRemove = _entities
                .Where(x => ids.Contains(x.Id));

            _deployerContext.RemoveRange(toRemove);
        }

        protected IQueryable<T> GetByIdQueryable(int id)
        {
            return _entities.Where(x => x.Id == id);
        }

        public virtual T Get(int id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }

        protected IQueryable<T> GetAllQueryable()
        {
            return _deployerContext.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _deployerContext.Set<T>().ToList();
        }



    }
}

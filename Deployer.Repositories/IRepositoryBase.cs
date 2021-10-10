using System;
using System.Collections.Generic;

namespace Deployer.Repositories
{
    public interface IRepositoryBase<T>
    {
        void Add(T entity);
        T Get(int id);
        List<T> GetAll();
        void Remove(int id);
        void Remove(List<int> ids);
        void SaveChanges();
    }
}
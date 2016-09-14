using System;
using System.Data.Entity;
using System.Linq;

namespace Selkie.Web.MicroServices.Common.Interfaces
{
    public interface IDbContext <T>
    {
        void Add(T instance);
        void AddOrUpdate(T instance);
        IQueryable <T> All();
        T Find(Guid id);
        void Remove(T instance);
        void SaveChanges();

        void SetStateForEntity(T instance,
                               EntityState state);
    }
}
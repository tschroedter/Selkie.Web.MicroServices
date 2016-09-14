using System;
using System.Linq;

namespace Selkie.Web.MicroServices.Common.Interfaces
{
    public interface IRepository <T>
        where T : IEntity
    {
        IQueryable <T> All { get; }
        T FindById(Guid id);
        void Remove(T entity);
        void Save(T instance);
        void Save();
    }
}
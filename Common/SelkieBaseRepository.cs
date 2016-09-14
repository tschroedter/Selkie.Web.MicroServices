using System;
using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.Common.Aspects;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.Common
{
    [Interceptor(typeof( ExceptionLoggerAspect ))]
    public abstract class SelkieBaseRepository <TType, TContext>
        : IRepository <TType>
        where TType : IEntity
        where TContext : IDbContext <TType>
    {
        protected SelkieBaseRepository([NotNull] TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; private set; }

        public IQueryable <TType> All
        {
            get
            {
                return Context.All();
            }
        }

        public TType FindById(Guid id)
        {
            return Context.Find(id);
        }

        public void Save(TType instance)
        {
            if ( instance.Id == default ( Guid ) )
            {
                instance.Id = Guid.NewGuid();
            }

            Context.AddOrUpdate(instance);
            Context.SaveChanges();
        }

        public void Remove(TType instance)
        {
            Context.Remove(instance);
            Context.SaveChanges();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
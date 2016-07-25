using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core;
using Selkie.Web.MicroServices.Common.Aspects;
using Selkie.Web.MicroServices.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Common
{
    [ExcludeFromCodeCoverage]
    [Interceptor(typeof( ExceptionLoggerAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public abstract class SelkieBaseContext <TInterface, TDbSet>
        : DbContext,
          ISelkieBaseContext <TInterface>
        where TInterface : class, IEntity
        where TDbSet : class
    {
        protected SelkieBaseContext()
            : base("name = SelkieDBConnectionString")
        {
        }

        public DbSet <TDbSet> SelkieDbSet { get; set; }

        public void Add(TInterface entity)
        {
            TDbSet instance = ConvertToInstance(entity);

            SelkieDbSet.Add(instance);
        }

        public IQueryable <TInterface> All()
        {
            var list = new List <TInterface>();

            // ReSharper disable once LoopCanBeConvertedToQuery => using LINQ throws exception
            foreach ( TDbSet dbSet in SelkieDbSet )
            {
                var asInterface = dbSet as TInterface;

                list.Add(asInterface);
            }

            return list.AsQueryable();
        }

        public TInterface Find(int id)
        {
            TDbSet dbSet = SelkieDbSet.Find(id);

            return dbSet as TInterface;
        }

        public void Remove(TInterface entity)
        {
            TDbSet instance = ConvertToInstance(entity);

            SelkieDbSet.Remove(instance);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public void SetStateForEntity(TInterface entity,
                                      EntityState state)
        {
            TDbSet instance = ConvertToInstance(entity);

            Entry(instance).State = EntityState.Modified;
        }

        protected virtual TDbSet ConvertToInstance(TInterface entity)
        {
            var instance = entity as TDbSet;

            if ( instance == null )
            {
                throw new ArgumentException("Provided 'entity' instance is not a '" + typeof( TDbSet ) + "'!",
                                            "entity");
            }

            return instance;
        }
    }
}
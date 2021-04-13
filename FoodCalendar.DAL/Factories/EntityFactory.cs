using System;
using System.Linq;
using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodCalendar.DAL.Factories
{
    public class EntityFactory
    {
        private readonly ChangeTracker _changeTracker;

        public EntityFactory(ChangeTracker changeTracker) => _changeTracker = changeTracker;

        public TEntity Create<TEntity>(Guid id) where TEntity : EntityBase, new()
        {
            TEntity entity;
            if (id != Guid.Empty)
            {
                entity = _changeTracker?.Entries<TEntity>()
                    .SingleOrDefault(i => i.Entity.Id == id)?.Entity;
                if (entity == null)
                {
                    entity = new TEntity {Id = id};
                    _changeTracker?.Context.Attach(entity);
                }
            }
            else
            {
                entity = new TEntity();
            }

            return entity;
        }
    }
}
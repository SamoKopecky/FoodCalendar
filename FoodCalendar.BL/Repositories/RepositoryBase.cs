using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Interfaces;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.BL.Repositories
{
    public class RepositoryBase<TModel, TEntity> : IRepository<TModel>
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
    {
        private readonly Func<TEntity, TModel> _mapEntityToModel;
        private readonly Func<TModel, TEntity> _mapModelToEntity;
        private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>> _includeChildEntities;
        private readonly IDbContextFactory _dbContextFactory;

        protected RepositoryBase(
            Func<TEntity, TModel> mapEntityToModel,
            Func<TModel, TEntity> mapModelToEntity,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeChildEntities,
            IDbContextFactory dbContextFactory
        )
        {
            _mapEntityToModel = mapEntityToModel;
            _mapModelToEntity = mapModelToEntity;
            _includeChildEntities = includeChildEntities;
            _dbContextFactory = dbContextFactory;
        }

        public void Delete(TModel model)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.Set<TEntity>();
            dbSet.Remove(_mapModelToEntity(model));
            dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.Set<TEntity>();
            var entity = new TEntity() {Id = id};
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public void InsertOrUpdate(TModel model)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var dbSet = dbContext.Set<TEntity>();
            var entity = _mapModelToEntity(model);
            if (!dbSet.Any(e => e.Equals(entity)))
            {
                dbSet.Add(entity);
            }
            else
            {
                dbSet.Update(entity);
            }

            dbContext.SaveChanges();
        }

        public TModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            IQueryable<TEntity> dbSet = dbContext.Set<TEntity>();
            var entity = dbSet.SingleOrDefault(e => e.Id == id);
            return _mapEntityToModel(entity);
        }

        public ICollection<TModel> GetAll()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            IQueryable<TEntity> dbSet = dbContext.Set<TEntity>();
            if (_includeChildEntities != null)
            {
                dbSet = _includeChildEntities(dbSet);
            }

            return dbSet.Select(e => _mapEntityToModel(e)).ToList();
        }
    }
}
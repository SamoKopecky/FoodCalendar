using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodCalendar.BL.Interfaces;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.BL.Repositories
{
    public class RepositoryBase<TModel, TEntity> : IRepository<TModel>
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
    {
        private readonly Func<TEntity, TModel> _mapEntityToModel;
        private readonly Func<TModel, TEntity> _mapModelToEntity;
        private readonly IDbContextFactory _dbContextFactory;
        private FoodCalendarDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        protected RepositoryBase(
            Func<TEntity, TModel> mapEntityToModel,
            Func<TModel, TEntity> mapModelToEntity,
            IDbContextFactory dbContextFactory
        )
        {
            _mapEntityToModel = mapEntityToModel;
            _mapModelToEntity = mapModelToEntity;
            _dbContextFactory = dbContextFactory;
        }

        private void ReloadContext()
        {
            this._dbContext = _dbContextFactory.CreateDbContext();
            this._dbSet = _dbContext.Set<TEntity>();
        }

        public void Delete(TModel model)
        {
            ReloadContext();
            _dbSet.Remove(_mapModelToEntity(model));
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            ReloadContext();
            var entity = new TEntity() {Id = id};
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void InsertOrUpdate(TModel model)
        {
            ReloadContext();
            var entity = _mapModelToEntity(model);
            if (!_dbSet.Any(e => e.Equals(entity)))
            {
                _dbSet.Add(entity);
            }
            else
            { 
                _dbSet.Update(entity);
            }

            _dbContext.SaveChanges();
        }

        public TModel GetById(Guid id)
        {
            ReloadContext();
            var entity = _dbSet.SingleOrDefault(e => e.Id == id);
            return _mapEntityToModel(entity);
        }

        public ICollection<TModel> GetAll()
        {
            ReloadContext();
            return _dbSet.Select(e => _mapEntityToModel(e)).ToList();
        }
    }
}
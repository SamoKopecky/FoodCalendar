using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.BL.Repositories
{
    public class DishRepository : RepositoryBase<DishModel, Dish>
    {
        public DishRepository(IDbContextFactory contextFactory) :
            base(
                DishMapper.MapEntityToModel,
                DishMapper.MapModelToEntity,
                entity => entity.Select(d => d)
                    .Include(d => d.Meals)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .Include(d => d.Meals)
                    .ThenInclude(m => m.Process),
                contextFactory
            )
        {
        }
    }
}
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.BL.Repositories
{
    public class MealRepository : RepositoryBase<MealModel, Meal>
    {
        public MealRepository(IDbContextFactory contextFactory) :
            base(
                MealMapper.MapEntityToModel,
                MealMapper.MapModelToEntity,
                entity => entity.Select(m => m)
                    .Include(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .Include(m => m.Process),
                contextFactory
            )
        {
        }
    }
}
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.BL.Repositories
{
    public class DayRepository : RepositoryBase<DayModel, Day>
    {
        public DayRepository(IDbContextFactory contextFactory) :
            base(
                DayMapper.MapEntityToModel,
                DayMapper.MapModelToEntity,
                entity => entity.Select(d => d)
                    .Include(d => d.Dishes)
                    .ThenInclude(dd => dd.Dish)
                    .ThenInclude(d => d.DishMeals)
                    .ThenInclude(dm => dm.Meal)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .Include(d => d.Dishes)
                    .ThenInclude(dd => dd.Dish)
                    .ThenInclude(d => d.DishMeals)
                    .ThenInclude(dm => dm.Meal)
                    .ThenInclude(m => m.Process),
                contextFactory
            )
        {
        }
    }
}
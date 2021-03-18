using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.BL.Repositories
{
    public class MealRepository : RepositoryBase<MealModel, Meal>
    {
        public MealRepository(IDbContextFactory contextFactory) :
            base(
                MealMapper.MapEntityToModel,
                MealMapper.MapModelToEntity,
                contextFactory
            )
        {
        }
    }
}
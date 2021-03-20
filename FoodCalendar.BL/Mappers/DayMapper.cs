using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.BL.Mappers
{
    public static class DayMapper
    {
        public static DayModel MapEntityToModel(Day entity)
        {
            if (entity != null)
            {
                return new DayModel()
                {
                    Id = entity.Id,
                    CaloriesLimit = entity.CaloriesLimit,
                    CaloriesSum = entity.CaloriesSum,
                    Date = entity.Date,
                    Dishes = entity.Dishes.Select(dd => DishMapper.MapEntityToModel(dd.Dish)).ToList(),
                };
            }

            return null;
        }

        public static Day MapModelToEntity(DayModel model)
        {
            if (model != null)
            {
                return new Day()
                {
                    Id = model.Id,
                    CaloriesLimit = model.CaloriesLimit,
                    CaloriesSum = model.CaloriesSum,
                    Date = model.Date,
                    Dishes = model.Dishes.Select(d => new DayDish() {Dish = DishMapper.MapModelToEntity(d)}).ToList()
                };
            }

            return null;
        }
    }
}
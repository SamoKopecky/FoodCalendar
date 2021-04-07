using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieCatalog.DAL.Factories;

namespace FoodCalendar.BL.Mappers
{
    public static class DishMapper
    {
        public static DishModel MapEntityToModel(Dish entity)
        {
            if (entity != null)
            {
                return new DishModel()
                {
                    Id = entity.Id,
                    Calories = entity.Calories,
                    DishName = entity.DishName,
                    TotalTime = entity.TotalTime,
                    DishTime = entity.DishTimeAndTime,
                    Meals = entity.DishMeals.Select(dm => MealMapper.MapEntityToModel(dm.Meal)).ToList()
                };
            }

            return null;
        }

        public static Dish MapModelToEntity(DishModel model, EntityFactory entityFactory)
        {
            var entity = (entityFactory ??= new EntityFactory()).Create<Dish>(model.Id);
            entity.Id = model.Id;
            entity.Calories = model.Calories;
            entity.DishName = model.DishName;
            entity.TotalTime = model.TotalTime;
            entity.DishTimeAndTime = model.DishTime;
            entity.DishMeals = model.Meals
                .Select(m => new DishMeal()
                {
                    Meal = MealMapper.MapModelToEntity(m, entityFactory), MealId = m.Id, DishId = model.Id,
                    Dish = entity
                }).ToList();
            return entity;
        }
    }
}
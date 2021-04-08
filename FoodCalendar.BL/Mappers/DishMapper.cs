using System;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;

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
                    DishTime = entity.DishTimeAndDate,
                    Meals = entity.Meals.Select(MealMapper.MapEntityToModel).ToList()
                };
            }

            return null;
        }

        public static Dish MapModelToEntity(DishModel model, EntityFactory entityFactory)
        {
            var entity = entityFactory.Create<Dish>(model.Id);
            entity.Id = model.Id;
            entity.Calories = model.Calories;
            entity.DishName = model.DishName;
            entity.TotalTime = model.TotalTime;
            entity.DishTimeAndDate = model.DishTime;
            entity.Meals = model.Meals.Select(m => MealMapper.MapModelToEntity(m, entityFactory)).ToList();
            return entity;
        }
    }
}
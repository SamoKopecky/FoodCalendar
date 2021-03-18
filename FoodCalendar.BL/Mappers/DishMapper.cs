using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;

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
                    DishTime = entity.DishTime,
                    Meals = entity.DishMeals.Select(dm => MealMapper.MapEntityToModel(dm.Meal)).ToList()
                };
            }

            return null;
        }

        public static Dish MapModelToEntity(DishModel model)
        {
            if (model != null)
            {
                return new Dish()
                {
                    Id = model.Id,
                    Calories = model.Calories,
                    DishName = model.DishName,
                    TotalTime = model.TotalTime,
                    DishTime = model.DishTime,
                    DishMeals = model.Meals.Select(m => new DishMeal() {Meal = MealMapper.MapModelToEntity(m)}).ToList()
                };
            }
            return null;
        }
    }
}
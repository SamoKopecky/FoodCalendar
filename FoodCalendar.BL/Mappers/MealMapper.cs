using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.BL.Mappers
{
    public static class MealMapper
    {
        public static MealModel MapEntityToModel(Meal entity)
        {
            if (entity != null)
            {
                return new MealModel()
                {
                    Id = entity.Id,
                    Calories = entity.Calories,
                    TotalTime = entity.TotalTime,
                    Process = ProcessMapper.MapEntityToModel(entity.Process),
                    IngredientsUsed = entity.IngredientsUsed.Select(IngredientAmountMapper.MapEntityToModel).ToList()
                };
            }

            return null;
        }


        public static Meal MapModelToEntity(MealModel model)
        {
            if (model != null)
            {
                return new Meal()
                {
                    Id = model.Id,
                    Calories = model.Calories,
                    TotalTime = model.TotalTime,
                    Process = ProcessMapper.MapModelToEntity(model.Process),
                    IngredientsUsed = model.IngredientsUsed.Select(IngredientAmountMapper.MapModelToEntity).ToList()
                };
            }

            return null;
        }
    }
}
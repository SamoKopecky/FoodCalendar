using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieCatalog.DAL.Factories;

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


        public static Meal MapModelToEntity(MealModel model, EntityFactory entityFactory)
        {
            var entity = (entityFactory ??= new EntityFactory()).Create<Meal>(model.Id);

            entity.Id = model.Id;
            entity.Calories = model.Calories;
            entity.TotalTime = model.TotalTime;
            entity.Process = ProcessMapper.MapModelToEntity(model.Process, entityFactory);
            entity.IngredientsUsed = model.IngredientsUsed
                .Select(ia => IngredientAmountMapper.MapModelToEntity(ia, entityFactory)).ToList();
            return entity;
        }
    }
}
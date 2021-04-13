using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;

namespace FoodCalendar.BL.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientModel MapEntityToModel(Ingredient entity)
        {
            if (entity != null)
            {
                return new IngredientModel()
                {
                    Id = entity.Id,
                    Calories = entity.Calories,
                    AmountStored = entity.AmountStored,
                    Name = entity.Name,
                    UnitName = entity.UnitName,
                };
            }

            return null;
        }

        public static Ingredient MapModelToEntity(IngredientModel model, EntityFactory entityFactory)
        {
            var entity = entityFactory.Create<Ingredient>(model.Id);
            entity.Id = model.Id;
            entity.Calories = model.Calories;
            entity.AmountStored = model.AmountStored;
            entity.Name = model.Name;
            entity.UnitName = model.UnitName;
            return entity;
        }
    }
}
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.BL.Mappers
{
    static class IngredientMapper
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

        public static Ingredient MapModelToEntity(IngredientModel model)
        {
            if (model != null)
            {
                return new Ingredient()
                {
                    Id = model.Id,
                    Calories = model.Calories,
                    AmountStored = model.AmountStored,
                    Name = model.Name,
                    UnitName = model.UnitName,
                };
            }

            return null;
        }
    }
}
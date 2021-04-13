using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;

namespace FoodCalendar.BL.Mappers
{
    public static class IngredientAmountMapper
    {
        public static IngredientAmountModel MapEntityToModel(IngredientAmount entity)
        {
            if (entity != null)
            {
                return new IngredientAmountModel()
                {
                    Id = entity.Id,
                    Amount = entity.Amount,
                    Ingredient = IngredientMapper.MapEntityToModel(entity.Ingredient)
                };
            }

            return null;
        }

        public static IngredientAmount MapModelToEntity(IngredientAmountModel model, EntityFactory entityFactory)
        {
            var entity = entityFactory.Create<IngredientAmount>(model.Id);
            entity.Id = model.Id;
            entity.Amount = model.Amount;
            entity.Ingredient = IngredientMapper.MapModelToEntity(model.Ingredient, entityFactory);
            return entity;
        }
    }
}
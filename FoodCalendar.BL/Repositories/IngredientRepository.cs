using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.BL.Repositories
{
    public class IngredientRepository : RepositoryBase<IngredientModel, Ingredient>
    {
        public IngredientRepository(IDbContextFactory contextFactory) :
            base(
                IngredientMapper.MapEntityToModel,
                IngredientMapper.MapModelToEntity,
                null,
                contextFactory
            )
        {
        }
    }
}
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.BL.Repositories
{
    public class IngredientAmountRepository : RepositoryBase<IngredientAmountModel, IngredientAmount>
    {
        public IngredientAmountRepository(IDbContextFactory contextFactory) :
            base(
                IngredientAmountMapper.MapEntityToModel,
                IngredientAmountMapper.MapModelToEntity,
                entity => entity.Select(ia => ia)
                    .Include(ia => ia.Ingredient),
                contextFactory
            )
        {
        }
    }
}
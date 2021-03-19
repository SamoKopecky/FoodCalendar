using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
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
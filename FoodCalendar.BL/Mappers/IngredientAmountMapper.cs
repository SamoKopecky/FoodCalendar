using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.BL.Interfaces;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;

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

        public static IngredientAmount MapModelToEntity(IngredientAmountModel model)
        {
            if (model != null)
            {
                return new IngredientAmount()
                {
                    Id = model.Id,
                    Amount = model.Amount,
                    Ingredient = IngredientMapper.MapModelToEntity(model.Ingredient)
                };
            }

            return null;
        }
    }
}
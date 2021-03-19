using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.BL.Repositories
{
    public class DishRepository : RepositoryBase<DishModel, Dish>
    {
        public DishRepository(IDbContextFactory contextFactory) :
            base(
                DishMapper.MapEntityToModel,
                DishMapper.MapModelToEntity,
                entity => entity.Select(d => d)
                    .Include(d => d.DishMeals)
                    .ThenInclude(dm => dm.Meal)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .Include(d => d.DishMeals)
                    .ThenInclude(dm => dm.Meal)
                    .ThenInclude(m => m.Process),
                contextFactory
            )
        {
        }
    }
}
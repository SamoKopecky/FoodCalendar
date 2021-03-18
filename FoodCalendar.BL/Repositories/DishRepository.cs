using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.BL.Repositories
{
    public class DishRepository : RepositoryBase<DishModel, Dish>
    {
        public DishRepository(IDbContextFactory contextFactory) :
            base(
                DishMapper.MapEntityToModel,
                DishMapper.MapModelToEntity,
                contextFactory
            )
        {
        }
    }
}
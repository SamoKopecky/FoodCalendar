using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Seeds;

namespace FoodCalendar.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedDb();
        }

        public static void SeedDb()
        {
            var dbContextFactory = new DbContextFactory();
            using var ctx = dbContextFactory.CreateDbContext();
            var repo = new DishRepository(dbContextFactory);
            var lunch = DishSeed.Lunch;
            repo.Insert(DishMapper.MapEntityToModel(lunch));
        }
    }
}
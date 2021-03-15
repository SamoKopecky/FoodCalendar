using System;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var ctx = new FoodCalendarDbContext();
            var ingredient = new Ingredient("test", 5, "kg", 10);
            ctx.Ingredients.Add(ingredient);
            ctx.SaveChanges();
        }
    }
}
using System;
using System.Linq;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var ctx = new FoodCalendarDbContext();
            var ingredient = new Ingredient("test", 5, "kg", 10);
//            ctx.Ingredients.Add(ingredient);
            ctx.SaveChanges();
            var data = ctx.Set<Ingredient>().AsEnumerable().Select(ingredient1 => ingredient1).ToArray();
            Console.WriteLine(data[0].Name);
            var inMemOptions = new DbContextOptionsBuilder<FoodCalendarDbContext>().UseInMemoryDatabase("test").Options;
            using var ctxInMem = new FoodCalendarDbContext(inMemOptions);
            ctxInMem.Ingredients.Add(ingredient);
            ctxInMem.SaveChanges();
            var dataInMem = ctxInMem.Set<Ingredient>().AsEnumerable().Select(ingredient1 => ingredient1).ToArray();
            Console.WriteLine(dataInMem[0].Name);
        }
    }
}
using System;
using System.Linq;
using System.Reflection.Metadata;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Enums;
using FoodCalendar.DAL.Migrations;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var ctx = new FoodCalendarDbContext();
            var ingredients = ctx.Ingredients.Where(i => i.Name == "test");
            foreach (var i in ingredients)
            {
                ctx.Ingredients.Remove(i);
            }

            var ingredientsAmounts = ctx.IngredientAmounts.Select(ia => ia);
            foreach (var ia in ingredientsAmounts)
            {
                ctx.IngredientAmounts.Remove(ia);
            }

            var ingredient = new Ingredient("test", 5, "kg", 10);
            ctx.Ingredients.Add(ingredient);
            var ingredientAmount = new IngredientAmount(2, ingredient);
            ctx.IngredientAmounts.Add(ingredientAmount);
            var ingredientAmount2 = new IngredientAmount(3, ingredient);
            ctx.IngredientAmounts.Add(ingredientAmount2);
            var process = new Process(5, "description");
            var food = new Meal()
            {
                Calories = 5, Process = process, IngredientsUsed = {ingredientAmount, ingredientAmount2}
            };
            ctx.Meals.Add(food);
            //ctx.Processes.Add(process);
            ctx.SaveChanges();
            /*var data = ctx.Set<Ingredient>().AsEnumerable().Select(ingredient1 => ingredient1).ToArray();
            Console.WriteLine(data[0].Name);
            var inMemOptions = new DbContextOptionsBuilder<FoodCalendarDbContext>().UseInMemoryDatabase("test").Options;
            using var ctxInMem = new FoodCalendarDbContext(inMemOptions);
            ctxInMem.Ingredients.Add(ingredient);
            ctxInMem.SaveChanges();
            var dataInMem = ctxInMem.Set<Ingredient>().AsEnumerable().Select(ingredient1 => ingredient1).ToArray();
            Console.WriteLine(dataInMem[0].Name);*/
        }
    }
}
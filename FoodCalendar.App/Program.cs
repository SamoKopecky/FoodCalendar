using System;
using System.Linq;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using Microsoft.EntityFrameworkCore.Internal;

namespace FoodCalendar.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dbContextFactory = new DbContextFactory();
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                var ingredient = new Ingredient("test", 5, "kg", 10);
                //ctx.Ingredients.Add(ingredient);
                var ingredientAmount = new IngredientAmount(2, ingredient);
                var ingredientAmount2 = new IngredientAmount(3, ingredient);
                var ingredientAmount3 = new IngredientAmount(3, ingredient);
                var process = new Process(5, "description");
                var process2 = new Process(5, "description");
                var food = new Meal()
                {
                    Calories = 5,
                    Process = process,
                    IngredientsUsed = {ingredientAmount, ingredientAmount2}
                };
                var food2 = new Meal()
                {
                    Calories = 6,
                    Process = process2,
                    IngredientsUsed = {ingredientAmount, ingredientAmount2}
                };
                //ctx.Meals.Add(food);
                //ctx.Processes.Add(process);
                var dish = new Dish("test dish", new DateTime(1999, 1, 1));

                dish.DishMeals.Add(new DishMeal()
                {
                    Meal = food
                });
                dish.DishMeals.Add(new DishMeal()
                {
                    Meal = food2
                });
                //ctx.Dishes.Add(dish);

                var day = new Day(10);
                day.Dishes.Add(new DayDish()
                {
                    Dish = dish
                });
                ctx.Days.Add(day);
                ctx.SaveChanges();
            }


            /*using var ctx2 = new FoodCalendarDbContext();
            var sameDish = ctx2.Set<Dish>().AsEnumerable().Select(d => d).ToArray()[0];
            var sameFood = ctx2.Set<Meal>().AsEnumerable().Select(d => d).ToArray()[0];


            sameDish.DishMeals.Add(new DishMeal()
            {
                Meal = sameFood
            });
            ctx2.Update(sameDish);
            ctx2.SaveChanges();*/
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
using System;
using FoodCalendar.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodCalendar.DAL.Tests
{
    [TestClass]
    public class DayTests
    {
        [TestMethod]
        public void SumCaloriesTest()
        {
            const int expected = 25;
            var day = new Day(150);
            var ingredient1 = new Ingredient("egg", 3, "ks", 5);
            var ingredient2 = new Ingredient("ham", 1, "ks", 5);
            var ingredient3 = new Ingredient("butter", 1, "ks", 5);
            var food = new Food();
            var desert = new Desert();
            desert.AddIngredient(ingredient3, 1);
            food.AddIngredient(ingredient1, 3);
            food.AddIngredient(ingredient2, 1);
            food.SumCalories();
            desert.SumCalories();
            var dish1 = new Dish("test", new DateTime());
            var dish2 = new Dish("test", new DateTime());
            dish1.Meals.Add(food);
            dish1.SumTimeAndCalories();
            dish2.Meals.Add(desert);
            dish2.SumTimeAndCalories();
            day.Dishes.Add(dish1);
            day.Dishes.Add(dish2);

            day.SumCalories();

            var actual = day.CaloriesSum;
            Assert.AreEqual(expected, actual);
        }
    }
}

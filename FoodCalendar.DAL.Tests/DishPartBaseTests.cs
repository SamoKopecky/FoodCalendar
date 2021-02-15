using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoodCalendar.DAL.Entities;


namespace FoodCalendar.DAL.Tests
{
    [TestClass]
    public class DishPartBaseTests
    {
        [TestMethod]
        public void SumDishPartBaseCaloriesTest()
        {
            const int expected = 25;
            var ingredient1 = new Ingredient("egg", 3, "ks", 5);
            var ingredient2 = new Ingredient("ham", 1, "ks", 5);
            var ingredient3 = new Ingredient("butter", 1, "ks", 5);
            var food = new Food();
            var desert = new Desert();
            desert.IngredientsUsed.Add(ingredient3, 1);
            food.IngredientsUsed.Add(ingredient1, 3);
            food.IngredientsUsed.Add(ingredient2, 1);
            food.IngredientsUsed.Add(desert, 1);

            food.SumCalories();

            var actual = food.Calories;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcessSetterTest()
        {
            const int expected = 120;
            var food = new Food();

            food.Process = new Process(120, "test");

            var actual = food.TotalTime;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ProcessSetterNegativeNumberTest()
        {
            var food = new Food();

            food.Process = new Process(-1, "test");
        }
    }
}
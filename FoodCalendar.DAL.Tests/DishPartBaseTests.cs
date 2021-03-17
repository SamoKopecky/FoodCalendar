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
            const int expected = 20;
            var ingredient1 = new Ingredient("egg", 3, "ks", 5);
            var ingredient2 = new Ingredient("ham", 1, "ks", 5);
            //var ingredient3 = new Ingredient("butter", 1, "ks", 5);
            var food = new Meal();
            //var desert = new Desert();
            //desert.AddIngredient(ingredient3, 1);
            food.AddIngredient(ingredient1, 3);
            food.AddIngredient(ingredient2, 1);
            //food.AddIngredient(desert, 1);

            food.SumCalories();

            var actual = food.Calories;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcessSetterTest()
        {
            const int expected = 180;
            var food = new Meal();

            food.Process = new Process(120, "test");
            food.Process = new Process(60, "test");

            var actual = food.TotalTime;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ProcessSetterNegativeNumberTest()
        {
            var food = new Meal();

            food.Process = new Process(-1, "test");
        }
    }
}
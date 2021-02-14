using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoodCalendar.DAL.Interfaces;
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
            var food1 = new Desert();
            var food2 = new Desert();
            food2.Ingredients.Add(ingredient3);
            food1.Ingredients.Add(ingredient1);
            food1.Ingredients.Add(ingredient2);
            food1.Ingredients.Add(food2);

            food1.SumCalories();

            var actual = food1.Calories;
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
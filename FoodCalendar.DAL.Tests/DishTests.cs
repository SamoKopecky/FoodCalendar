﻿using System;
using FoodCalendar.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodCalendar.DAL.Tests
{
    [TestClass]
    public class DishTests
    {
        [TestMethod]
        public void SumCaloriesTest()
        {
            const int caloriesExpected = 25;
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
            var dish = new Dish("test", new DateTime());
            dish.Meals.Add(food);
            dish.Meals.Add(desert);

            dish.SumTimeAndCalories();

            int caloriesActual = dish.Calories;
            Assert.AreEqual(caloriesExpected, caloriesActual);
        }

        [TestMethod]
        public void SumTimeTest()
        {
            const int timeExpected = 130;
            var food = new Food();
            var desert = new Desert();
            food.Process = new Process(60, "test");
            desert.Process = new Process(70, "test");
            var dish = new Dish("test", new DateTime());
            dish.Meals.Add(food);
            dish.Meals.Add(desert);

            dish.SumTimeAndCalories();

            int timeActual = dish.TotalTime;
            Assert.AreEqual(timeExpected, timeActual);
        }
    }
}
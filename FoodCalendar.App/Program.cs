using System;
using System.Collections;
using System.Collections.Generic;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.App
{
    class Program
    {
        static void Main(string[] args)
        {   
            // Testing DAL model
            Ingredient ingredient1 = new Ingredient("ryza", 100, "g", 2);
            Ingredient ingredient2 = new Ingredient("olej", 100, "ml", 5);
            Dish dishTest = new Dish(10, "test", new DateTime(), 5);
            Procces proccesTest = new Procces(200, "popis");
            Food foodTest = new Food(proccesTest, 1, proccesTest.TimeRequired);

            Ingredient ingredient3 = new Ingredient("maslo", 50, "g", 30);
            Food masloFood = new Food(new Procces(150, "popis_maslo"), 5, 150);
            masloFood.ingredients.Add(ingredient3);

            foodTest.ingredients.Add(ingredient2);
            foodTest.ingredients.Add(ingredient1);
            foodTest.ingredients.Add(masloFood);
            dishTest.foods.Add(foodTest);
            List<Dish> dishes = new List<Dish>();
            dishes.Add(dishTest);
            Day day = new Day(15, 10, dishes);
        }
    }
}

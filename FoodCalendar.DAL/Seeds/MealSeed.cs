﻿using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class MealSeed
    {
        public static readonly Meal HamAndEggs = new Meal()
        {
            Calories = 10,
            TotalTime = 60,
            ProcessId = ProcessSeed.HamAndEggsProcess.Id,
            Process = ProcessSeed.HamAndEggsProcess,
            IngredientsUsed = new List<IngredientAmount>()
                {IngredientAmountSeed.EggAmount, IngredientAmountSeed.HamAmount}
        };
    }
}
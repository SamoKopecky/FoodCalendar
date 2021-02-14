using System;
using FoodCalendar.DAL.Interfaces;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public abstract class DishPartBase<T> : EntityBase, IDishPart
    {
        private Process _process;

        public Process Process
        {
            get => _process;
            set
            {
                if (value.TimeRequired <= 0) throw new DivideByZeroException();
                TotalTime = value.TimeRequired;
                _process = value;
            }
        }

        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<T> DishPartTypes { get; set; }
        public ICollection<IDishPart> Ingredients { get; set; }

        private void Initialize()
        {
            Ingredients = new List<IDishPart>();
            DishPartTypes = new List<T>();
            TotalTime = 0;
        }

        protected DishPartBase()
        {
            Initialize();
            Calories = 0;
        }

        protected DishPartBase(int calories)
        {
            Initialize();
            Calories = calories;
        }

        public void SumCalories()
        {
            Calories = CycleSumCalories(this);
        }

        private int CycleSumCalories(DishPartBase<T> dishPart)
        {
            var sum = 0;
            foreach (var element in dishPart.Ingredients)
            {
                if (element is Ingredient ingredient)
                {
                    sum += ingredient.Calories * ingredient.Amount;
                }
                else
                {
                    sum += CycleSumCalories((DishPartBase<T>) element);
                }
            }

            return sum;
        }
    }
}
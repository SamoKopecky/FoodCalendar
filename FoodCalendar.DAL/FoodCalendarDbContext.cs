﻿using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL
{
    public class FoodCalendarDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishMeal> DishMeals { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<DayDish> DayDishes { get; set; }

        public FoodCalendarDbContext()
        {
        }

        public FoodCalendarDbContext(DbContextOptions<FoodCalendarDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=192.168.1.220;
                    Database = FoodCalendar;
                    MultipleActiveResultSets = True;
                    User ID =SA;
                    Password = Casi0Casi0;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientAmount>()
                .HasOne(ia => ia.Ingredient)
                .WithMany(i => i.IngredientAmounts);

            modelBuilder.Entity<Meal>()
                .HasMany(f => f.IngredientsUsed)
                .WithOne(ia => ia.Meal);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.Meal)
                .WithOne(f => f.Process);

            modelBuilder.Entity<DishMeal>()
                .HasKey(dm => new {dm.DishId, dm.MealId});
            modelBuilder.Entity<DishMeal>()
                .HasOne(dm => dm.Dish)
                .WithMany(d => d.DishMeals);
            modelBuilder.Entity<DishMeal>()
                .HasOne(dm => dm.Meal)
                .WithMany(m => m.DishMeals);

            modelBuilder.Entity<DayDish>()
                .HasKey(dd => new {dd.DayId, dd.DishId});
            modelBuilder.Entity<DayDish>()
                .HasOne(dd => dd.Day)
                .WithMany(day => day.Dishes);
            modelBuilder.Entity<DayDish>()
                .HasOne(dd => dd.Dish)
                .WithMany(dish => dish.DayDishes);
        }
    }
}
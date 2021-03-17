﻿// <auto-generated />
using System;
using FoodCalendar.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodCalendar.DAL.Migrations
{
    [DbContext(typeof(FoodCalendarDbContext))]
    partial class FoodCalendarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CaloriesLimit")
                        .HasColumnType("int");

                    b.Property<int>("CaloriesSum")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.DayDish", b =>
                {
                    b.Property<Guid>("DayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DayId", "DishId");

                    b.HasIndex("DishId");

                    b.ToTable("DayDishes");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<string>("DishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DishTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.DishMeal", b =>
                {
                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MealId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DishId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("DishMeals");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AmountStored")
                        .HasColumnType("int");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.IngredientAmount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid?>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MealId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MealId");

                    b.ToTable("IngredientAmounts");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<Guid>("ProcessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId")
                        .IsUnique();

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Process", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeRequired")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.DayDish", b =>
                {
                    b.HasOne("FoodCalendar.DAL.Entities.Day", "Day")
                        .WithMany("Dishes")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodCalendar.DAL.Entities.Dish", "Dish")
                        .WithMany("DayDishes")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.DishMeal", b =>
                {
                    b.HasOne("FoodCalendar.DAL.Entities.Dish", "Dish")
                        .WithMany("DishMeals")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodCalendar.DAL.Entities.Meal", "Meal")
                        .WithMany("DishMeals")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.IngredientAmount", b =>
                {
                    b.HasOne("FoodCalendar.DAL.Entities.Ingredient", "Ingredient")
                        .WithMany("IngredientAmounts")
                        .HasForeignKey("IngredientId");

                    b.HasOne("FoodCalendar.DAL.Entities.Meal", "Meal")
                        .WithMany("IngredientsUsed")
                        .HasForeignKey("MealId");

                    b.Navigation("Ingredient");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Meal", b =>
                {
                    b.HasOne("FoodCalendar.DAL.Entities.Process", "Process")
                        .WithOne("Meal")
                        .HasForeignKey("FoodCalendar.DAL.Entities.Meal", "ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Process");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Day", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Dish", b =>
                {
                    b.Navigation("DayDishes");

                    b.Navigation("DishMeals");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Ingredient", b =>
                {
                    b.Navigation("IngredientAmounts");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Meal", b =>
                {
                    b.Navigation("DishMeals");

                    b.Navigation("IngredientsUsed");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Process", b =>
                {
                    b.Navigation("Meal");
                });
#pragma warning restore 612, 618
        }
    }
}

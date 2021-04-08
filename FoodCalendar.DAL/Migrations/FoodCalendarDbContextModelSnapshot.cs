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

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<string>("DishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DishTimeAndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
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

                    b.Property<Guid?>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProcessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

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
                    b.HasOne("FoodCalendar.DAL.Entities.Dish", "Dish")
                        .WithMany("Meals")
                        .HasForeignKey("DishId");

                    b.HasOne("FoodCalendar.DAL.Entities.Process", "Process")
                        .WithOne("Meal")
                        .HasForeignKey("FoodCalendar.DAL.Entities.Meal", "ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Dish", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Ingredient", b =>
                {
                    b.Navigation("IngredientAmounts");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Meal", b =>
                {
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

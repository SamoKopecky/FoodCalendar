﻿// <auto-generated />
using System;
using FoodCalendar.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodCalendar.DAL.Migrations
{
    [DbContext(typeof(FoodCalendarDbContext))]
    [Migration("20210317103149_AddFoodTest")]
    partial class AddFoodTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Food", b =>
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

                    b.ToTable("Foods");
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

                    b.Property<Guid?>("FoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientAmounts");
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

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Food", b =>
                {
                    b.HasOne("FoodCalendar.DAL.Entities.Process", "Process")
                        .WithOne("Food")
                        .HasForeignKey("FoodCalendar.DAL.Entities.Food", "ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Process");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.IngredientAmount", b =>
                {
                    b.HasOne("FoodCalendar.DAL.Entities.Food", "Food")
                        .WithMany("IngredientsUsed")
                        .HasForeignKey("FoodId");

                    b.HasOne("FoodCalendar.DAL.Entities.Ingredient", "Ingredient")
                        .WithMany("IngredientAmounts")
                        .HasForeignKey("IngredientId");

                    b.Navigation("Food");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Food", b =>
                {
                    b.Navigation("IngredientsUsed");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Ingredient", b =>
                {
                    b.Navigation("IngredientAmounts");
                });

            modelBuilder.Entity("FoodCalendar.DAL.Entities.Process", b =>
                {
                    b.Navigation("Food");
                });
#pragma warning restore 612, 618
        }
    }
}

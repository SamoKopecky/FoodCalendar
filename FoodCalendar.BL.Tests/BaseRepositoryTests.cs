using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Factories;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Seeds;
using Xunit;

namespace FoodCalendar.BL.Tests
{
    public class BaseRepositoryTests
    {
        [Fact]
        public void Insert_Dish_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var dish = DishMapper.MapEntityToModel(DishSeed.Lunch);

            repo.Insert(dish);

            var dbDish = repo.GetAll().First();

            Assert.Equal(dish, dbDish, DishModel.DishModelComparer);
        }


        [Fact]
        public void Insert_DuplicateIngredient_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new IngredientRepository(dbContextFactory);
            var ingredient = IngredientMapper.MapEntityToModel(IngredientSeed.Egg);
            ingredient.Id = new Guid("e3db45b6-5493-4876-b771-765e580b71c9");
            var ingredients = new List<IngredientModel> {ingredient};

            repo.Insert(ingredient);
            repo.Insert(ingredient);

            var dbDishes = repo.GetAll().ToList();
            Assert.Equal(ingredients, dbDishes, IngredientModel.IngredientModelComparer);
        }

        [Fact]
        public void Update_IngredientAmount_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var dish = DishMapper.MapEntityToModel(DishSeed.Lunch);

            repo.Insert(dish);

            var updatedDish = repo.GetAll().First();
            updatedDish.Meals.First().IngredientsUsed.First().Ingredient.Calories = 2054;
            repo.Update(updatedDish);
            Assert.Equal(repo.GetAll().First(), updatedDish, DishModel.DishModelComparer);
        }

        [Fact]
        public void GetAll_Dish_List()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var dishOne = DishMapper.MapEntityToModel(DishSeed.Lunch);
            var dishTwo = DishMapper.MapEntityToModel(DishSeed.Lunch);
            dishTwo.Calories = 500;
            var newDishes = new List<DishModel>() {dishTwo, dishOne};
            newDishes[0].Id = new Guid("de1123d5-e47d-4a3a-9f6f-3ebe70deafd3");
            newDishes[1].Id = new Guid("e3db45b6-5493-4876-b771-765e580b71c9");

            repo.Insert(newDishes[0]);
            repo.Insert(newDishes[1]);

            var dbDishes = repo.GetAll().ToList();
            Assert.Equal(newDishes, dbDishes, DishModel.DishModelComparer);
        }

        [Fact]
        public void DeleteById_Dish_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var dish = DishMapper.MapEntityToModel(DishSeed.Lunch);
            dish.Id = new Guid("8abb44cb-e3e6-4850-8028-2418424123da");
            repo.Insert(dish);

            repo.Delete(dish.Id);

            var dishes = repo.GetAll();
            Assert.Empty(dishes);
        }

        [Fact]
        public void DeleteByModel_Dish_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var dish = DishMapper.MapEntityToModel(DishSeed.Lunch);
            dish.Id = new Guid("8abb44cb-e3e6-4850-8028-2418424123da");
            repo.Insert(dish);

            repo.Delete(dish);

            var dishes = repo.GetAll();
            Assert.Empty(dishes);
        }

        [Fact]
        public void GetById_Dish_one()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var dish = DishMapper.MapEntityToModel(DishSeed.Lunch);
            var id = new Guid("8abb44cb-e3e6-4850-8028-2418424123da");
            dish.Id = id;

            repo.Insert(dish);

            var dbDish = repo.GetById(id);
            Assert.Equal(dish, dbDish, DishModel.DishModelComparer);
        }
    }
}
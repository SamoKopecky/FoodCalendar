using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCalendar.DAL.Migrations
{
    public partial class AddCascadeOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAmounts_Ingredients_IngredientId",
                table: "IngredientAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAmounts_Meals_MealId",
                table: "IngredientAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Dishes_DishId",
                table: "Meals");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAmounts_Ingredients_IngredientId",
                table: "IngredientAmounts",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAmounts_Meals_MealId",
                table: "IngredientAmounts",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Dishes_DishId",
                table: "Meals",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAmounts_Ingredients_IngredientId",
                table: "IngredientAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAmounts_Meals_MealId",
                table: "IngredientAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Dishes_DishId",
                table: "Meals");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAmounts_Ingredients_IngredientId",
                table: "IngredientAmounts",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAmounts_Meals_MealId",
                table: "IngredientAmounts",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Dishes_DishId",
                table: "Meals",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

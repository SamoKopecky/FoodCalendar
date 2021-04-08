using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCalendar.DAL.Migrations
{
    public partial class RemoveManyToManyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishMeals");

            migrationBuilder.AddColumn<Guid>(
                name: "DishId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DishId",
                table: "Meals",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Dishes_DishId",
                table: "Meals",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Dishes_DishId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_DishId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "DishId",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "DishMeals",
                columns: table => new
                {
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishMeals", x => new { x.DishId, x.MealId });
                    table.ForeignKey(
                        name: "FK_DishMeals_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishMeals_MealId",
                table: "DishMeals",
                column: "MealId");
        }
    }
}

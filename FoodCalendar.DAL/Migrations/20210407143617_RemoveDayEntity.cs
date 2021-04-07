using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCalendar.DAL.Migrations
{
    public partial class RemoveDayEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayDishes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishMeals",
                table: "DishMeals");

            migrationBuilder.RenameColumn(
                name: "DishTime",
                table: "Dishes",
                newName: "DishTimeAndTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishMeals",
                table: "DishMeals",
                columns: new[] { "DishId", "MealId", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DishMeals",
                table: "DishMeals");

            migrationBuilder.RenameColumn(
                name: "DishTimeAndTime",
                table: "Dishes",
                newName: "DishTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishMeals",
                table: "DishMeals",
                columns: new[] { "DishId", "MealId" });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaloriesLimit = table.Column<int>(type: "int", nullable: false),
                    CaloriesSum = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayDishes",
                columns: table => new
                {
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayDishes", x => new { x.DayId, x.DishId });
                    table.ForeignKey(
                        name: "FK_DayDishes_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayDishes_DishId",
                table: "DayDishes",
                column: "DishId");
        }
    }
}

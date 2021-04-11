using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCalendar.DAL.Migrations
{
    public partial class AddMealNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MealName",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealName",
                table: "Meals");
        }
    }
}

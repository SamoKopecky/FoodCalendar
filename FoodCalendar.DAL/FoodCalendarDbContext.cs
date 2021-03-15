using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL
{
    public class FoodCalendarDbContext : DbContext
    {
        public FoodCalendarDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=192.168.1.220;
                Database = FoodCalendar;
                MultipleActiveResultSets = True;
                User ID =SA;
                Password = Casi0Casi0;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        
    }
}
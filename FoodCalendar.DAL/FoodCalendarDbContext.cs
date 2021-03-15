using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL
{
    public class FoodCalendarDbContext : DbContext
    {
        public FoodCalendarDbContext()
        {
        }

        public FoodCalendarDbContext(DbContextOptions<FoodCalendarDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Process> Processes { get; set; }
    }
}
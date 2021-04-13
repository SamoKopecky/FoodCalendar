using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL
{
    public class FoodCalendarDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public FoodCalendarDbContext()
        {
        }

        public FoodCalendarDbContext(DbContextOptions<FoodCalendarDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=172.105.249.59;
                    Database = FoodCalendar;
                    MultipleActiveResultSets = True;
                    User ID =SA;
                    Password = Yq47KJ#*zXEhT%E;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientAmount>()
                .HasOne(ia => ia.Ingredient)
                .WithMany(i => i.IngredientAmounts)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meal>()
                .HasMany(m => m.IngredientsUsed)
                .WithOne(ia => ia.Meal)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Process>()
                .HasOne(p => p.Meal)
                .WithOne(m => m.Process)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dish>()
                .HasMany(d => d.Meals)
                .WithOne(m => m.Dish)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
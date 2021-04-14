using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL.Factories
{
    public class DbContextFactory : IDbContextFactory
    {
        /// <summary>
        /// Creates a db context for real applications.
        /// </summary>
        /// <returns>The created db context.</returns>
        public FoodCalendarDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodCalendarDbContext>()
                .UseSqlServer(@"Server=192.168.1.220;
                    Database = FoodCalendar;
                    MultipleActiveResultSets = True;
                    User ID =SA;
                    Password = Yq47KJ#*zXEhT%E;", options => options.EnableRetryOnFailure())
                .EnableSensitiveDataLogging();
            return new FoodCalendarDbContext(optionsBuilder.Options);
        }
    }
}
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL.Factories
{
    public class DbContextFactory : IDbContextFactory
    {
        public FoodCalendarDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodCalendarDbContext>();
            optionsBuilder.UseSqlServer(
                @"Server=192.168.1.220;
                    Database = FoodCalendar;
                    MultipleActiveResultSets = True;
                    User ID =SA;
                    Password = Yq47KJ#*zXEhT%E;");
            optionsBuilder.EnableSensitiveDataLogging();
            return new FoodCalendarDbContext(optionsBuilder.Options);
        }
    }
}
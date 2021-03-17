using FoodCalendar.DAL.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodCalendar.DAL.Tests
{
    [TestClass]
    class DbContextTests
    {
        private readonly IDbContextFactory _ctxFactory = new InMemoryDbContextFactory();

        public void OneToOne_ProcessToMeal()
        {
            using var ctx = _ctxFactory.CreateDbContext();
        }
    }
}
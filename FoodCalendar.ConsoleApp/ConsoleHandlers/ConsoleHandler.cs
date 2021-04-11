using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public abstract class ConsoleHandler
    {
        protected readonly IDbContextFactory DbContextFactory;
        protected readonly int IdLength;

        protected ConsoleHandler(IDbContextFactory dbContextFactory, int idLength)
        {
            DbContextFactory = dbContextFactory;
            IdLength = idLength;
        }
    }
}
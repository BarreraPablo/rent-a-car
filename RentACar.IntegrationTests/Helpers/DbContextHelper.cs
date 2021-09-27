using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.IntegrationTests.Helpers
{
    public class DbContextHelper
    {
        public static RentACarContext GetRentACarContext()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables()
                .Build();

            return new RentACarContext(new DbContextOptionsBuilder<RentACarContext>()
            .UseSqlServer(configuration["ConnectionStrings:RentACar"])
            .Options);
        }

        public static void DeleteRange<T>(IEnumerable<T> list) 
        {
            using (var context = GetRentACarContext())
            {
                foreach (var item in list)
                {
                    context.Remove(item);

                }
                context.SaveChanges();
            }
        }
    }
}

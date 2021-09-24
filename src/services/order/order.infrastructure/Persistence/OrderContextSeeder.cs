using Microsoft.Extensions.Logging;
using order.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order.infrastructure.Persistence
{
    public class OrderContextSeeder
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeeder> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "vinhnhan", FirstName = "Nhan", LastName = "Vinh", EmailAddress = "tranvinhnhan@gmail.com", AddressLine = "HCM", Country = "Vietnam", TotalPrice = 350 }
            };
        }
    }
}

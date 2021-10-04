using Microsoft.Extensions.Logging;
using order.domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.infrastructure.Persistence
{
    public class OrderContextSeeder
    {
        public static async Task SeedAsync<TContext>(OrderContext orderContext, ILogger<TContext> logger)
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
                new Order()
                {
                    UserName = "vinhnhan",
                    TotalPrice = 350,
                    FirstName = "Nhan",
                    LastName = "Vinh",
                    EmailAddress = "tranvinhnhan@gmail.com",
                    AddressLine = "HCM",
                    Country = "Vietnam",
                    State = "HCM",
                    ZipCode = "70000",
                    CardName  = "Tran Vinh Nhan",
                    CardNumber  = "0101010101010101",
                    Expiration  = "02/29",
                    CVV = "235" ,
                    PaymentMethod = 1
                }
            };
        }
    }
}

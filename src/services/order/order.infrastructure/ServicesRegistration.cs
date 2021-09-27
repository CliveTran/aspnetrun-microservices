using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using order.application.Contracts.Infrastructure;
using order.application.Contracts.Repositories;
using order.application.Models;
using order.infrastructure.Persistence;
using order.infrastructure.Repositories;
using order.infrastructure.Services;

namespace order.infrastructure
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("OrderConnectionString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.Configure<EmailSettings>(_ => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

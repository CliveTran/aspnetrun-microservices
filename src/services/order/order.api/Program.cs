using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using order.api.Extensions;
using order.application;
using order.infrastructure;
using order.infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "order.api", Version = "v1" });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "order.api v1"));
}

app.UseAuthorization();

app.MapControllers();

app.MigrateData<OrderContext>((dbContext, services) => 
{
    var logger = services.GetRequiredService<ILogger<OrderContext>>();
    OrderContextSeeder.SeedAsync(dbContext, logger).Wait();
});

app.Run();

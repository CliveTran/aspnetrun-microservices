using basket.api.Application.Interfaces;
using basket.api.Grpc;
using basket.api.Infrastructure.Repositories;
using discount.grpc.Protos;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "basket.api", Version = "v1" });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
        options => options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")));

builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();

builder.Services.AddMassTransit(config => config.UsingRabbitMq());
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "basket.api v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();

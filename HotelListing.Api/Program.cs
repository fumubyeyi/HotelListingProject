using Serilog;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using HotelListing.Api.Services;
using HotelListing.Api.Weather;
using HotelListing.Api.Data;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'AZURE_SQL_CONNECTIONSTRING' not found.");
builder.Services.AddDbContext<HotelListingDbContext>(options =>
    options.UseSqlServer(connectionString ,sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    }));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
});
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();

app.UseCors();
app.UseHttpsRedirection();

var weatherForecastEndpoints = new WeatherForecastEndpoints(app.Services.GetRequiredService<IWeatherForecastService>());
weatherForecastEndpoints.MapWeatherForecastEndpoints(app);

app.Run();

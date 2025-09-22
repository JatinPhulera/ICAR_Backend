using Microsoft.OpenApi.Models;
using ICAR.Scanner.WebApi.Automapper;
using ICAR.Scanner.DataAccess.Context;
using ICAR.Scanner.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using ICAR.Scanner.Services.Services.TreeService;
using ICAR.Scanner.Services.Services.UserService;
using ICAR.Scanner.Services.Services.SensorService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// var corsPolicy = "policy";

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ICARDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
);

builder.Services.AddICARAutoMapper(); // Registers AutoMapper and your generic service
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuditTreeService, AuditTreeService>();
builder.Services.AddScoped<ITREESService, TREESService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();



// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:5191") // Angular frontend origin
              .AllowAnyMethod() // Allow GET, POST, PUT, DELETE
              .AllowAnyHeader() // Allow headers like Authorization, Content-Type
              .AllowCredentials(); // Allow credentials (e.g., for JWT authentication)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection(); // Optional, remove if not using HTTPS
app.UseCors("AllowAngular"); // Apply CORS policy before Authorization
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");
app.MapGet("/", () => "Welcome to ICAR.Scanner.WebApi!");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

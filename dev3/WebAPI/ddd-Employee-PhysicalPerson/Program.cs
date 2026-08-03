using Scalar.AspNetCore;
using ddd_Employee_PhysicalPerson.Application;
using ddd_Employee_PhysicalPerson.Infra;
using ddd_Employee_PhysicalPerson.Domain;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// This is option 1 to bind the configuration section to the RedisSettings class
// builder.Services.AddOptions<RedisSettings>().BindConfiguration("RedisSettings");

// This is option 2.
// Step 1. Fetch your custom configuration section into a local variable
var redisSettings = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>();

// Step 2. Register the connection as a Singleton service
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var options = new ConfigurationOptions
    {
        EndPoints = { { redisSettings.Host, redisSettings.Port } },
        User = redisSettings.User,
        Password = redisSettings.Password
    };

    // Add the host and port endpoints cleanly
    // options.EndPoints.Add(redisSettings.Host, redisSettings.Port);

    // Create and return the live connection
    return ConnectionMultiplexer.Connect(options);
});

// // // https://share.google/aimode/sM7VS3qfY2Tx9ZvXX

builder.Services.AddOpenApi();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPhysicalPersonService, PhysicalPersonService>();

builder.Services.AddScoped<IRedisRepository, RedisRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRedisRepository>();
builder.Services.AddScoped<IPhysicalPersonRepository, PhysicalPersonRedisRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // This was added from Nuget //dotnet add package Scalar.AspNetCore
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

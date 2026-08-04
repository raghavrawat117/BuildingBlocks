using EmployeeAPI.Services;
using EmployeeAPI.Models;
using FluentValidation;
using EmployeeAPI.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// MongoDB configuration
builder.Services.Configure<EmployeeDatabaseSettings>(
builder.Configuration.GetSection("EmployeeDatabase"));

//Ably configuration
builder.Services.Configure<AblySettings>(
builder.Configuration.GetSection("AblySettings")    
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

//register validator
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();

// Dependency Injection
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IEventPublisher,AblyEventPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
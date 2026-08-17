using EmployeeAPI.Models;
using FluentValidation;
using EmployeeAPI.Validators;
using FluentValidation.AspNetCore;
using EmployeeAPI.Middleware;
using EmployeeAPI.Repositories.Employee_Repository;
using EmployeeAPI.Repositories.Event_Publisher_Repository;
using EmployeeAPI.Services.EmployeeService;
using EmployeeAPI.HostedService;
using EmployeeAPI.Repositories;
using EmployeeAPI.Repositories.Template_Repository;
using EmployeeAPI.Services.TransformationService;

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
builder.Services.AddSingleton<IEventPublisher, AblyEventPublisher>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
builder.Services.AddScoped<ITransformationService, TransformationService>();

// Register the AblyEventSubscriber as a hosted service
builder.Services.AddHostedService<AblyEventSubscriber>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
using EmployeeAPI.Services;
using EmployeeAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmployeeDatabaseSettings>(
builder.Configuration.GetSection("EmployeeDatabase"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEmployeeService,
                           EmployeeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
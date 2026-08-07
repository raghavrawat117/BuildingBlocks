using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using System.Net;

namespace EmployeeAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Not found exception occurred");
            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (DatabaseConnectionException ex)
        {
            _logger.LogError(ex, "Database connection exception occurred");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.ServiceUnavailable,
                ex.Message);
        }
        catch (InvalidIdException ex)
        {
            _logger.LogError(ex, "Invalid exception occurred");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }

        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception occurred");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType =
            "application/json";

        context.Response.StatusCode =
            (int)statusCode;

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message,
            TraceId = context.TraceIdentifier
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
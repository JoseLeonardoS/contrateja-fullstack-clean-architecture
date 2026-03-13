using ContrateJa.Domain.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace ContrateJa.API.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
        => _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleAsync(context, HttpStatusCode.BadRequest, "Validation Error",
                string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)));
        }
        catch (NotFoundException ex)
        {
            await HandleAsync(context, HttpStatusCode.NotFound, "Not Found", ex.Message);
        }
        catch (ConflictException ex)
        {
            await HandleAsync(context, HttpStatusCode.Conflict, "Conflict", ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            await HandleAsync(context, HttpStatusCode.Unauthorized, "Unauthorized", ex.Message);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message);
        }
    }

    private static async Task HandleAsync(HttpContext context, HttpStatusCode statusCode, string error, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = (int)statusCode,
            error,
            message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
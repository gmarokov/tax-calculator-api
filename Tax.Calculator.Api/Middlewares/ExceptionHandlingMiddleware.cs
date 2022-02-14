using System.Net;
using FluentValidation;
using Newtonsoft.Json;

namespace Tax.Calculator.Api.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context,  RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context, ex);
        }
    }

    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = GetStatusCodeByExceptionType(exception);
        var result = JsonConvert.SerializeObject(new
        {
            StatusCode = statusCode,
            ErrorMessage = exception.Message
        });
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(result);
    }

    private static int GetStatusCodeByExceptionType(Exception exception)
    {
        var httpStatusCode = exception switch
        {
            ValidationException => (int) HttpStatusCode.BadRequest,
            _ => (int) HttpStatusCode.InternalServerError
        };

        return httpStatusCode;
    }
}

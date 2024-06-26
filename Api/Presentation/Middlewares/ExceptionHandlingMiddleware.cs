﻿using System.Diagnostics;
using System.Text.Json;
using Api.Domain;
using Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error occurred!",
            Detail = exception.Demystify().ToString(),
            Instance = $"demo:error:{Guid.NewGuid()}"
        };

        switch (exception)
        {
            case InfrastructureException:
                problemDetails.Status = 503;
                problemDetails.Title = exception.Message;
                break;

            case DomainException:
                problemDetails.Status = 501;
                problemDetails.Title = exception.Message;
                break;

            default:
                problemDetails.Status = 500;
                break;
        }

        _logger.LogError("An unexpected error occurred {@problemDetails}", problemDetails);

        await WriteProblemDetailsAsync(context, problemDetails);
    }

    private static Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails problemDetails)
    {
        problemDetails.Status ??= 500;
        var problem = JsonSerializer.Serialize(problemDetails);
        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/problem+json";
        return context.Response.WriteAsync(problem);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
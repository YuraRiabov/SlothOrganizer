﻿using SlothOrganizer.Domain.Exceptions;

namespace SlothOrganizer.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            if (exception is BaseException customException && customException.StatusCode != 0)
            {
                context.Response.StatusCode = customException.StatusCode;
            }
            else
            {
                context.Response.StatusCode = 500;
            }
            await context.Response.WriteAsync(exception.Message);
        }
    }
}

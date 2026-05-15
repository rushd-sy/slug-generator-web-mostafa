using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;


namespace SlugGeneratorWeb.Middlewares
{
    internal sealed class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception,
                "Exception occurred: Trace Id: {TraceIdentifier}, {Message}. Request Path: {Path}.",
                httpContext.TraceIdentifier,
                exception.Message,
                httpContext.Request.Path);

            var (status, title, detail) = exception switch
            {
                ValidationException validationException => (
                    StatusCodes.Status400BadRequest,
                    "Validation Failed",
                    validationException.Message
                ),
                ArgumentException => (
                    StatusCodes.Status400BadRequest,
                    "Bad Request",
                    exception.Message),
                _ => (StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "Unexpected error from server side")
            };

            httpContext.Response.StatusCode = status;

            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Type = exception.GetType().Name,
                Detail = detail
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using ShopifyApi.Common;
using System.Net;
using System.Text.Json;

namespace ShopifyApi.Middleware
{
    public class GlobalExecptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExecptionHandler> _logger;

        public GlobalExecptionHandler(ILogger<GlobalExecptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
            )
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            var response = exception switch
            {
                //fluentValidation  return 400 with errors
                ValidationException validationEx => new ApiErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Validation failed",
                    Errors = validationEx.Errors.Select(e => e.ErrorMessage),
                    TraceId = httpContext.TraceIdentifier
                },

                //KeyNOtFoundExection  returns 404
                KeyNotFoundException => new ApiErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = exception.Message,
                    TraceId = httpContext.TraceIdentifier
                },

                //ANything else returns 500
                _ => new ApiErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An unexpected error occurred",
                    TraceId = httpContext.TraceIdentifier
                }
            };

            httpContext.Response.StatusCode = response.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(response),
                cancellationToken);

            return true;
        }
    }
}

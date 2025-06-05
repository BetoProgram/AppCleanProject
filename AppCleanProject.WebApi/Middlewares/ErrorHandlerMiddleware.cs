using AppCleanProject.Application.Commons.Exceptions;
using Newtonsoft.Json;
using System.Net;
using WatchDog;

namespace AppCleanProject.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionsHanlderAsync(context, ex, _logger);
            }
        }

        private async Task ExceptionsHanlderAsync(HttpContext context, Exception ex, ILogger<ErrorHandlerMiddleware> logger)
        {
            object? errors = null;
            switch (ex)
            {
                case CustomException me:
                    logger.LogError(ex, "Errors in methods");
                    errors = me.Errores;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;
                case ValidationException validationFluentValidation:
                    logger.LogError(ex, "Errors in Validations");
                    errors = validationFluentValidation.Errors;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case Exception e:
                    logger.LogError(ex, "Errors in Server Application");
                    errors = new
                    {
                        message = "Error in Server Application"
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    WatchLogger.LogError(string.IsNullOrWhiteSpace(e.Message) ? "Errors in Server Application"
                        : string.IsNullOrWhiteSpace(e.InnerException!.Message) ? "" : $"{e.Message} {e.InnerException} {e.StackTrace}");
                    break;
            }

            context.Response.ContentType = "application/json";
            string resultados = string.Empty;

            if (errors != null)
            {
                resultados = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(resultados);
            }

        }
    }
}

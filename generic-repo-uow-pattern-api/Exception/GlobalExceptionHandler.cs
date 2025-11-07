using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace generic_repo_pattern_api.Exception
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "A timeout occurred");

            if (exception is TimeoutException)
            {
                await MyException.ExceptionMessage(httpContext, exception, 
                    HttpStatusCode.RequestTimeout,"A timeout occurred");
             
                return true;
            }

            if (exception is ArgumentException)
            {
                await MyException.ExceptionMessage(httpContext, exception,
                     HttpStatusCode.BadRequest, "A bad request occurred");

                return true;
            }
            else
            {
                await MyException.ExceptionMessage(httpContext, exception,
                    HttpStatusCode.InternalServerError, "An" + "Unexpected error  occurred");

                return true;
            }

            return false;

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace generic_repo_pattern_api.Exception
{
    public class MyException
    {
        public static async Task ExceptionMessage(HttpContext httpContext,
           System.Exception exception, HttpStatusCode httpStatusCode, string Title)
        {
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)httpStatusCode,
                Type = exception.GetType().Name,
                Title = Title,
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            });
        }
    }
}

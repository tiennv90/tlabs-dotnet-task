using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TestApp.Server.Filters
{
    public class CommonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("Global exception caught: " + context.Exception.Message);
            var exception = context.Exception;
            if (exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    error = exception.Message
                });
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    error = "Internal server error",
                    details = exception.Message
                })
                {
                    StatusCode = 500
                };
            }
            context.ExceptionHandled = true;
        }
    }
}
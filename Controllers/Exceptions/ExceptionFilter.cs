using System.Runtime.Serialization;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using BeetleTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace BeetleTracker.Controllers.Exceptions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if(context.Exception is EntityNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) statusCode;
            context.Result = new JsonResult(new
            {
                error = new[] {context.Exception.Message},
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
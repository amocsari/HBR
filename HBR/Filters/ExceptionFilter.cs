using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HBR.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            context.ExceptionHandled = true;

            if (context.Exception is HbrException)
            {
                context.Result = new ObjectResult(context.Exception.Message) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            else
            {
                context.Result = new ObjectResult(context.Exception?.ToString()) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}

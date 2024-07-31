using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace CommonLibrary
{
    public class ErrorHandling : IMiddleware
    {



        private static async Task HandlingException(HttpContext context, Exception e)
        {
            var error = new ErrorResponse();

            switch (e)
            {
                case BadRequestException:
                    error.Code = (int)HttpStatusCode.BadRequest;
                    error.Status = HttpStatusCode.BadRequest.ToString();
                    error.Message = e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFound:
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Status = HttpStatusCode.NotFound.ToString();
                    error.Message = e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(error);
            await context.Response.WriteAsync(json);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandlingException(context, e);

            }
        }

    }
}

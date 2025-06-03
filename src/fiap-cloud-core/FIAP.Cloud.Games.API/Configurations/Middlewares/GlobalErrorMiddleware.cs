using FIAP.Cloud.Games.Core.Responses.Https;
using System.Net;

namespace FIAP.Cloud.Games.API.Configurations.Middlewares
{
    public class GlobalErrorMiddleware(ILogger<GlobalErrorMiddleware> logger, RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new Response500Error());
            }
        }
    }
}

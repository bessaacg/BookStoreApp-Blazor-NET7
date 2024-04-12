using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //mimicking the try catch block that i want to add to every API
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorID = Guid.NewGuid();

                //Log this exception
                logger.LogError(ex, $"{errorID} : {ex.Message}");

                //Return a custom error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    id = errorID,
                    message = "Something went wrong! We are looking into resolving this.",
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}

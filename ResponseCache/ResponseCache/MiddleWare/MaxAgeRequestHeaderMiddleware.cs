using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;

namespace ResponseCache.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MaxAgeRequestHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public MaxAgeRequestHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }


        public Task Invoke(HttpContext httpContext)
        {

            var headers = httpContext.Request.GetTypedHeaders();
            headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(15)
            };

            // httpContext.Response.Headers.Append("Cache-Control", string.Format("public,max-age={0}", TimeSpan.FromHours(12).TotalSeconds));

            // int age = int.Parse(_configuration["MaxAge"]);
            // httpContext.Request.Headers.Add("Cache-Control", $"public,max-age={age}");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MaxAgeRequestHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseMaxAgeRequestHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MaxAgeRequestHeaderMiddleware>();
        }
    }
}

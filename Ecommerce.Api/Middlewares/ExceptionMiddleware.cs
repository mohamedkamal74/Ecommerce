using Ecommerce.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Api.Midelwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _rateLimltWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache cache)
        {
            _next = next;
            _environment = environment;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!IsRequestAllowed(context))
                {

                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var response = new ApiException((int)HttpStatusCode.TooManyRequests
                               , "Too Many Requests ,Please Try Again Later");

                    await  context.Response.WriteAsJsonAsync(response);
                }
                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _environment.IsDevelopment()
                   ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!)
                   : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip=context.Connection?.RemoteIpAddress?.ToString();
            var cacheKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timeStamp, count) = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimltWindow;
                return (dateNow, 0);
            });

            if (dateNow - timeStamp < _rateLimltWindow)
            {
                if (count >= 8) return false;
                _cache.Set(cacheKey, (timeStamp, count += 1), _rateLimltWindow);
            }
            else
            {
                _cache.Set(cacheKey, (timeStamp, count), _rateLimltWindow);
            }
            return true;
        }

        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frmae-Options"] = "DENY";

        }
    }
}

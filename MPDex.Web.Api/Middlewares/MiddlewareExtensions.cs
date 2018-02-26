using Microsoft.AspNetCore.Builder;

namespace MPDex.Web.Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRemoteIpAddressLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RemoteIpAddressLogging>();
        }
    }
}

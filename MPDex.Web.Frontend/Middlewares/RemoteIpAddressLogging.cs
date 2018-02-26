using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace MPDex.Web.Frontend.Middlewares
{
    public class RemoteIpAddressLogging
    {
        private readonly RequestDelegate _next;

        public RemoteIpAddressLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("Address", context.Connection.RemoteIpAddress))
            {
                await _next.Invoke(context);
            }
        }
    }
}

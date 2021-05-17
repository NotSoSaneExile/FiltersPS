using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Filters
{
    public class CustomFilterAttributes : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var ip = context.HttpContext.Connection.RemoteIpAddress;
            ip = System.Net.Dns.GetHostEntry(ip).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            ip.ToString();

            var result = context.Result;

            if(result is PageResult)
            {
                var page = ((PageResult)result);
                page.ViewData["filterMessage"] = $"Yours IP: {ip}";
            }
            await next.Invoke();
        }
    }
}

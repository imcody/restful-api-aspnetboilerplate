using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace ResponsibleSystem.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                //.ConfigureLogging((host, builder) =>
                //{
                //    builder.SetMinimumLevel(LogLevel.Trace);
                //})
                .UseStartup<Startup>()
                //.UseNLog()
                .Build();
        }
    }
}

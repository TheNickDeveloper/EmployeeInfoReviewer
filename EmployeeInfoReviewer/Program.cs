using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeInfoReviewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddFile(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .UseStartup<Startup>();
    }
}

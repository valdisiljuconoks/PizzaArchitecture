using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Mediating.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
                                                                      .ConfigureLogging((ctx, logging) =>
                                                                                        {
                                                                                            logging.AddConsole();
                                                                                            logging.AddDebug();
                                                                                        })
                                                                      .UseStartup<Startup>()
                                                                      .Build();
    }
}

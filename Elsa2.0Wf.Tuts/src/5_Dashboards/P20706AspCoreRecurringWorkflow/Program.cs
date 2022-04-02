using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace P20706AspCoreRecurringWorkflow
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    //.UseStaticWebAssets() // See note "Blank Page?!".
                    .UseStartup<Startup>());
    }
}
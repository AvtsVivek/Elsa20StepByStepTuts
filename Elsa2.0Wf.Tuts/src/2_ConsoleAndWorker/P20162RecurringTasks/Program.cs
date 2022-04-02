using System;
using System.Threading.Tasks;
using Elsa;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20162RecurringTasks
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Here we go...");

            var referenceUrlString = $"https://elsa-workflows.github.io/elsa-core/docs/next/guides/guides-recurring-tasks#create-console-project";
            Console.WriteLine("This example is picked from the following elsa guide url");
            Console.WriteLine(referenceUrlString);
            var host = new HostBuilder()
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime()
                .Build();

            using (host)
            {
                await host.StartAsync();
                await host.WaitForShutdownAsync();
            }
        }

        private static void ConfigureServices(IServiceCollection services) =>
            services
                .AddElsa(elsa => elsa
                    .AddConsoleActivities()
                    .AddQuartzTemporalActivities()
                    .AddWorkflow<HeartbeatWorkflow>());

    }
}

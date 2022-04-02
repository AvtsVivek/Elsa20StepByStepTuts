using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System.Threading.Tasks;

namespace P20130RecurringWorkflow
{

    internal static class Program
    {
        private static async Task Main() => await CreateHostBuilder()
            .UseConsoleLifetime()
            .Build()
            .RunAsync();

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices(
                    (_, services) =>
                    {
                        services
                            .AddElsa(elsa => elsa
                                .AddConsoleActivities()
                                .AddWorkflow<HelloWorldBasicWorkflow>()
                                .StartWorkflow<HelloWorldBasicWorkflow>()
                                )
                            //.AddHostedService<Worker>()
                            ;
                    });
    }
    // Demonistrates the StartWorkflow extension method from Configure Services method.
    // If we dont want to use StartWorkflow method, then we have to use a IHostedService worker
    // keep either one of the StartWorkflow or AddHostedService and commentout the other.
    // No point in keeping both.
}
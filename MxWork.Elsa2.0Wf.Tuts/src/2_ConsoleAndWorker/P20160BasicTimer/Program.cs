using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System.Threading.Tasks;

namespace P20160BasicTimer
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
                                .AddQuartzTemporalActivities()
                                //.AddWorkflow<TimerWorkflow>()
                                .StartWorkflow<TimerWorkflow>()
                                );
                    });
    }

    // There is something in the Timer(see inside of the recurringactivity activity that makes the workflow kick off.
    // If we comment out the timer activity, then the workflow would not start by itself.
    // We need a starter or runner in such a case.
}
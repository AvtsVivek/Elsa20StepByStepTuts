using Elsa;
using Elsa.Serialization;
using Elsa.Server.Api.Services;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Open.Linq.AsyncExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace P20270SleepingWorkflow
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("This example demos a sleeping workflow.");
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddActivity<Sleep>()
                    .AddQuartzTemporalActivities()                    
                    .AddWorkflow<SimpleSleepingWorkflow>())
                .BuildServiceProvider();

            // Run startup actions (will index triggers such as SignalReceived from workflows).
            //var startupRunner = services.GetRequiredService<IStartupRunner>();
            //await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow. 
            //SingletonTimerWorkflow

            await workflowStarter.BuildAndStartWorkflowAsync<SimpleSleepingWorkflow>();
            Console.ReadLine();
        }
    }
}
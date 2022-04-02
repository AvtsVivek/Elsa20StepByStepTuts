using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20050SimpleWhileLoop
{
    class Program
    {
        private static async Task Main()
        {            
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<SimpleWhileWorkflow>())
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<SimpleWhileWorkflow>();
            Console.ReadLine();

        }
    }
}

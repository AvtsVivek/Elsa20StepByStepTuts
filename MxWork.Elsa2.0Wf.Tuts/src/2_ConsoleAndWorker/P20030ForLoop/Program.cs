using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20030ForLoop
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<ForLoopWorkflow>())
                .BuildServiceProvider();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<ForLoopWorkflow>();
            Console.ReadLine();
        }
    }
}
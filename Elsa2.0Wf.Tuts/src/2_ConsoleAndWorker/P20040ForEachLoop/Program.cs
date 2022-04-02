using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20040ForEachLoop
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<ForEachLoopWorkflow>())
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).

            // The above statement implies that the following runner is needed. 
            // But working even without a runner. Not clear why.
            // Need to find out what is the role of the startup runner.

            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<ForEachLoopWorkflow>();
            Console.ReadLine();

        }
    }
}

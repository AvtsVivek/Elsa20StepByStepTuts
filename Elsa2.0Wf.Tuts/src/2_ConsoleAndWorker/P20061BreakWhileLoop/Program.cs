using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20061BreakWhileLoop
{
    /// <summary>
    /// Demonstrates the use of declarative composite activities.
    /// </summary>
    class Program
    {
        static async Task Main()
        {

            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<BreakoutWorkflow>()
                    )
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).

            // The above statement implies that the following runner is needed. 
            // But working even without a runner. Not clear why.
            // Need to find out what is the role of the startup runner.

            //var startupRunner = services.GetRequiredService<IStartupRunner>();
            //await startupRunner.StartupAsync();

            // Get a workflow runner.
            //var workflowRunner = services.GetRequiredService<IWorkflowRunner>();

            // Execute the workflow.
            //await workflowRunner.RunWorkflowAsync<BreakoutWorkflow>();

            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<BreakoutWorkflow>();

            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }

    }
}

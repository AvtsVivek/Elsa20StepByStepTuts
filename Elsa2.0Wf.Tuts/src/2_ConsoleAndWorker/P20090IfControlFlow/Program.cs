using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Activities.UserTask.Extensions;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System.Linq;

//IfControlFlowActivityDemo

namespace P20090IfControlFlow
{
    class Program
    {
        // Demonistrates an if control flow activity
        private static async Task Main()
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<IfControlFlowDemoWorkflow>())
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<IfControlFlowDemoWorkflow>();

            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }
    }
}

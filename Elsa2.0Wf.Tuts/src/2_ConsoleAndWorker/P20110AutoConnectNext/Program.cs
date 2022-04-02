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
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

//IfControlFlowActivityDemo
//GoBackDemoActivity
//GoBackDemoWorkflow
namespace P20110AutoConnectNext
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
                    .AddActivity<AutoConnectActivity>()
                    .AddWorkflow<AutoConnectWorkflow>())
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<AutoConnectWorkflow>();

            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }
    }
}

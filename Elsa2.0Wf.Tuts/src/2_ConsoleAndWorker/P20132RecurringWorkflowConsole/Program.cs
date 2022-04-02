using Elsa;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P20132RecurringWorkflowConsole
{
    class Program
    {
        private static async Task Main()
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddQuartzTemporalActivities()
                    .AddWorkflow<RecurringTaskWorkflow>())
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<RecurringTaskWorkflow>();

            var myList = new List<int>();


            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }
    }
}
using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Activities.UserTask.Extensions;
using System.Linq;
using System.Collections.Generic;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20080UserTaskConsole
{
    class Program
    {
        private static async Task Main()
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                //.AddSingleton<Usre>()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddUserTaskActivities()
                    .AddWorkflow<UserTaskWorkflow>()
                    )
                .BuildServiceProvider();

            //// Run startup actions (not needed when registering Elsa with a Host).
            //var startupRunner = services.GetRequiredService<IStartupRunner>();
            //await startupRunner.StartupAsync();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // And an interruptor.
            var workflowTriggerInterruptor = services.GetRequiredService<IWorkflowTriggerInterruptor>();

            // Execute the workflow.
            var runWorkflowResult = await workflowStarter.BuildAndStartWorkflowAsync<UserTaskWorkflow>();

            var availableActionsList = new List<string>();
            availableActionsList.AddRange(new[]
            {
                "Accept",
                "Reject",
                "Needs Work"
            });

            // Workflow is now halted on the user task activity. Ask user for input:
            Console.WriteLine($"What action will you take? Choose one of: {string.Join(", ", availableActionsList)}");
            var userAction = Console.ReadLine();
            var currentActivityId = runWorkflowResult.WorkflowInstance!.BlockingActivities.Select(i => i.ActivityId).First();
            await workflowTriggerInterruptor.InterruptActivityAsync(runWorkflowResult.WorkflowInstance, currentActivityId, userAction);
        }
    }
}

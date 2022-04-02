using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20161BasicTimer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddConsoleActivities()
                    .AddQuartzTemporalActivities()
                    .AddWorkflow<TimerWorkflow>()
                    )
                .BuildServiceProvider();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<TimerWorkflow>();
            Console.ReadLine();
        }
    }
}

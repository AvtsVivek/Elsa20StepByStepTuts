using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Elsa;

namespace P20011StartupRunner
{
    class Program
    {
        static async Task Main()
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddWorkflow<HelloWorldBasicWorkflow>())
                .BuildServiceProvider();


            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<HelloWorldBasicWorkflow>();

            Console.ReadLine();
        }
    }
}

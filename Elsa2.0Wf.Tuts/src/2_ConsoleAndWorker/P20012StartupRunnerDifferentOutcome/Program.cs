using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace P20012StartupRunnerDifferentOutcome
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddActivity<HelloWorldWithSomeotherOutcome>()
                    .AddActivity<GoodByeWorld>()
                    .AddWorkflow<HelloWorldWithSomeotherOutcomeWorkflow>())
                .BuildServiceProvider();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            await workflowStarter.BuildAndStartWorkflowAsync<HelloWorldWithSomeotherOutcomeWorkflow>();
            Console.ReadLine();
        }
    }
}
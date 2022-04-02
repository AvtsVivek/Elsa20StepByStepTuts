using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace P20031SimpleForkConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddActivity<ForkBranchDecisionActivity>()
                    .AddWorkflow<SimpleForkWorkflow>())
                .BuildServiceProvider();

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.

            Console.WriteLine("The fork activity in the fork workflow will schedule all branches");
            Console.WriteLine("See the following references");
            Console.WriteLine("https://stackoverflow.com/q/67636364/1977871");
            Console.WriteLine("https://github.com/elsa-workflows/elsa-core/issues/1026");
            Console.WriteLine("See also the example P20032SimpleSwitchConsole");
            // 

            await workflowStarter.BuildAndStartWorkflowAsync<SimpleForkWorkflow>();
            Console.WriteLine("Done. Good bye");
            Console.ReadLine();
        }
    }
}
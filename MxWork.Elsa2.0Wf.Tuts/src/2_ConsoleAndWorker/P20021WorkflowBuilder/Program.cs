using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace P20021WorkflowBuilder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddActivity<HelloWorld>()
                    .AddActivity<GoodByeWorld>()
                    //.AddWorkflow<HelloWorldWorkflow>())
                    )
                .AddAutoMapperProfiles<Program>()
                .BuildServiceProvider();

            // Not clear what the method AddAutoMapperProfiles does. Need to find out.

            // Run startup actions (not needed when registering Elsa with a Host).

            // The above statement implies that the following runner is needed. 
            // But working even without a runner. Not clear why.
            // Need to find out what is the role of the startup runner.

            //var startupRunner = services.GetRequiredService<IStartupRunner>();
            //await startupRunner.StartupAsync();

            // Build a new workflow.
            var workflowBuilder = services.GetRequiredService<IWorkflowBuilder>();

            var workflowBluePrint = workflowBuilder
                .StartWith<HelloWorld>()
                .Then<GoodByeWorld>()
                .Build()
                ;

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.StartWorkflowAsync(workflowBluePrint);

            Console.ReadLine();
        }
    }
}

using System;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Builders;

namespace P20020WorkflowBuilder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities())
                .BuildServiceProvider();

            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Build a new workflow.
            // Not clear about workflowBlue print. Need to find out more.
            var workflowBluePrint = services.GetRequiredService<IWorkflowBuilder>()
                .WriteLine("What's your name?")
                .ReadLine()
                .WriteLine(context => $"Greetings, {context.Input}!")
                .WriteLine("Bye!!")
                .Build();

            // Get a workflow runner.
            var workflowStarter = services.GetRequiredService<IStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.StartWorkflowAsync(workflowBluePrint);


            Console.ReadLine();

            //References. The read line activity is here.
            //https://github.com/elsa-workflows/elsa-core/tree/master/src/activities/Elsa.Activities.Console/Activities/ReadLine
        }
    }
}

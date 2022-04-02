using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.WorkflowsInCode;
using System;
using System.Threading.Tasks;

namespace P20250WorkflowDefinedInCode
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("This example demos how to create a workflow in code and run it.");
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    //.AddWorkflowsFrom<SignalReceiverWorkflow>()
                    )
                .BuildServiceProvider();

            // Run startup actions (will index triggers such as SignalReceived from workflows).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            var workflowDefinition = new SimpleWorkflowDefinedInCode().GetWorkflowDefWithTwoActivities();

            // Materialize workflow.
            var materializer = services.GetRequiredService<IWorkflowBlueprintMaterializer>();
            var workflowBluePrint = await materializer.CreateWorkflowBlueprintAsync(workflowDefinition);

            // Get workflow starter.
            var workflowStarter = services.GetRequiredService<IStartsWorkflow>();
            // Execute the workflow.
            await workflowStarter.StartWorkflowAsync(workflowBluePrint);


            // One more exercise.
            Console.WriteLine("One more exerciese. Type any key to continue");
            Console.ReadLine();

            workflowDefinition = new SimpleWorkflowDefinedInCode().GetWorkflowDef();
            workflowBluePrint = await materializer.CreateWorkflowBlueprintAsync(workflowDefinition);
            await workflowStarter.StartWorkflowAsync(workflowBluePrint);

            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }
    }
}
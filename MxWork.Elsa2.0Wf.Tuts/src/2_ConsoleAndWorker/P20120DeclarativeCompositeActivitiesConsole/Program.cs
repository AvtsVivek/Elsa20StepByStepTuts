using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elsa;
using Elsa.Models;
using Elsa.Serialization;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;

namespace P20120DeclarativeCompositeActivitiesConsole
{
    /// <summary>
    /// Demonstrates the use of declarative composite activities.
    /// </summary>
    class Program
    {
        static async Task Main()
        {
            // this example does not work with the current latest version of nuget package
            // Which is 2.0.0-preview7.1545.
            // But with the latest code this works. See the elsa git hub repo.

            // This is not working.... :(

            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options.AddConsoleActivities()
                //.AddActivity<CompositeActivity>()
                )
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Define a workflow.
            var workflowDefinition = new WorkflowDefinition
            {
                Id = "1",
                DefinitionId = "SampleWorkflow",
                Version = 1,
                IsPublished = true,
                IsLatest = true,
                PersistenceBehavior = WorkflowPersistenceBehavior.Suspended,
                Activities = new[]
                {
                    WriteLine("write-line-1", "==Composite Activities Demo=="),
                    new CompositeActivityDefinition
                    {
                        ActivityId = "composite-1",
                        Activities = new[]
                        {
                            WriteLine("write-line-2", "Line 1 of composite activity."),
                            WriteLine("write-line-3", "Line 2 of composite activity."),
                        },
                        Connections = new[]
                        {
                            new ConnectionDefinition("write-line-2", "write-line-3", OutcomeNames.Done)
                        }
                    },
                    WriteLine("write-line-4", "==End=="),
                },
                Connections = new[]
                {
                    new ConnectionDefinition("write-line-1", "composite-1", OutcomeNames.Done),
                    new ConnectionDefinition("composite-1", "write-line-4", OutcomeNames.Done),
                }
            };

            // Serialize workflow definition to JSON.
            var serializer = services.GetRequiredService<IContentSerializer>();
            var json = serializer.Serialize(workflowDefinition);

            // Deserialize workflow definition from JSON.
            var deserializedWorkflowDefinition = serializer.Deserialize<WorkflowDefinition>(json);

            // Materialize workflow.
            var materializer = services.GetRequiredService<IWorkflowBlueprintMaterializer>();
            var workflowBluePrint = await materializer.CreateWorkflowBlueprintAsync(deserializedWorkflowDefinition);

            // Execute workflow.
            //var workflowRunner = services.GetRequiredService<IStartsWorkflow>();
            //await workflowRunner.StartWorkflowAsync(workflowBlueprint);

            // Get a workflow starter.
            var workflowStarter = services.GetRequiredService<IStartsWorkflow>();

            // Execute the workflow.
            await workflowStarter.StartWorkflowAsync(workflowBluePrint);

            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }

        private static ActivityDefinition WriteLine(string id, string text) =>
            new()
            {
                ActivityId = id,
                Type = nameof(Elsa.Activities.Console.WriteLine),
                Properties = new List<ActivityDefinitionProperty>()
                {
                    ActivityDefinitionProperty.Liquid(nameof(Elsa.Activities.Console.WriteLine.Text), text)
                }
            };
    }
}

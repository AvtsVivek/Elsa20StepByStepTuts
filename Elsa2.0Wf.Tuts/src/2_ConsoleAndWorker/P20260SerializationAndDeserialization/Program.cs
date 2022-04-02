using Elsa;
using Elsa.Serialization;
using Elsa.Server.Api.Services;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Open.Linq.AsyncExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace P20260SerializationAndDeserialization
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("This example demos how to serialize a given workflow.");
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    .AddConsoleActivities()
                    .AddQuartzTemporalActivities()                    
                    .AddWorkflow<HeartbeatWorkflow>())
                .AddElsaApiEndpoints()
                .BuildServiceProvider();

            // Run startup actions (will index triggers such as SignalReceived from workflows).
            var startupRunner = services.GetRequiredService<IStartupRunner>();

            await startupRunner.StartupAsync();

            var workflowRegistry = services.GetRequiredService<IWorkflowRegistry>();

            //var workflowBlueprint = await workflowRegistry.GetAsync(id, null, versionOptions.Value, cancellationToken);
            var workflowBlueprints = await workflowRegistry.ListActiveAsync().ToList();
            //var workflowBlueprintList = await workflowRegistry.ListAsync().ToList();

            if (workflowBlueprints.ToList().Count == 0)
            {
                Console.WriteLine("Something wrong. Exiting");
                return;
            }
            
            var workflowBlueprintMapper = services.GetRequiredService<IWorkflowBlueprintMapper>();           
            var workflowBlueprintModel = await workflowBlueprintMapper.MapAsync(workflowBlueprints[0]);
            var contentSerializer = services.GetRequiredService<IContentSerializer>();
            var jsonString = contentSerializer.Serialize(workflowBlueprintModel);
            Console.WriteLine(jsonString);
            Console.WriteLine("Type a key to exit the program..");
            Console.ReadLine();
        }
    }
}
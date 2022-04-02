using Elsa;
using Elsa.Persistence;
using Elsa.Persistence.Specifications.WorkflowInstances;
using Elsa.Persistence.YesSql;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System;
using System.Threading.Tasks;

namespace P20450PersistenceYesSql
{
    class Program
    {
        // Look into the following folder.
        // P20450PersistenceYesSql\bin\Debug\net5.0
        // You should find a elsa.yessql.db
        // 

        private static async Task Main()
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    // Configure Elsa to use the Entity Framework Core persistence provider using one of the three available providers 
                    .UseYesSqlPersistence()
                    .AddConsoleActivities()
                    .AddWorkflow<HelloWorldPersistanceWorkflow>())
                    //.AddAutoMapperProfiles<Program>()
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            // Get a workflow runner.
            var workflowRunner = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            var runWorkflowResult = await workflowRunner.BuildAndStartWorkflowAsync<HelloWorldPersistanceWorkflow>();

            // Get a reference to the workflow instance store.
            var store = services.GetRequiredService<IWorkflowInstanceStore>();

            // Count the number of workflow instances of HelloWorld.
            var count = await store.CountAsync(new WorkflowDefinitionIdSpecification(nameof(HelloWorldPersistanceWorkflow)));

            Console.WriteLine(count);

            var loadedWorkflowInstance = await store.FindByIdAsync(runWorkflowResult.WorkflowInstance.Id);
            Console.WriteLine(loadedWorkflowInstance);
        }
    }
}

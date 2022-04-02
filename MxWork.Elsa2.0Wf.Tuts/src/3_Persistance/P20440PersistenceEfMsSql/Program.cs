using System;
using System.Threading.Tasks;
using Elsa.Persistence;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.Specifications.WorkflowInstances;
using Elsa.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Elsa.Persistence.EntityFramework.SqlServer;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Elsa;

namespace P20440PersistenceEfMsSql
{
    class Program
    {
        private static async Task Main()
        {
            // Create a service container with Elsa services.
            var services = new ServiceCollection()
                .AddElsa(options => options
                    // Configure Elsa to use the Entity Framework Core persistence provider using one of the three available providers 
                    .UseEntityFrameworkPersistence(ef =>
                    {
                        ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa20;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    })
                    .AddConsoleActivities()
                    //.AddWorkflow<HelloWorldPersistanceWorkflow>()
                    )
                .AddAutoMapperProfiles<Program>()
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            var workflowRegistry = services.GetRequiredService<IWorkflowRegistry>();
            

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

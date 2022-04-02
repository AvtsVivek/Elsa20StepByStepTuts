using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System;
using System.Threading.Tasks;

//P20445ContentApprovalPersistenceEfMsSql
//P20245ContentApprovalPersistenceEfMsSql
namespace P20445ContentApprovalPersistenceEfMsSql
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var config = builder.Build();

            var elsaSection = config.GetSection("Elsa");

            var services = new ServiceCollection()
                .AddElsa(options => options
                // Configure Elsa to use the Entity Framework Core persistence provider using one of the three available providers 
                .UseEntityFrameworkPersistence(ef =>
                {
                    ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa202;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                })
                .AddConsoleActivities()
                .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                .AddWorkflow<DocumentApprovalWorkflow>())
                .AddAutoMapperProfiles<Program>() // Not sure what this does.
                .BuildServiceProvider();

            // Run startup actions (not needed when registering Elsa with a Host).
            var startupRunner = services.GetRequiredService<IStartupRunner>();
            await startupRunner.StartupAsync();

            var workflowRegistry = services.GetRequiredService<IWorkflowRegistry>();


            // Get a workflow runner.
            var workflowRunner = services.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Run the workflow.
            var runWorkflowResult = await workflowRunner.BuildAndStartWorkflowAsync<DocumentApprovalWorkflow>();


            // This example is not currently working.
            // This is created based on the example from the following.
            // https://elsa-workflows.github.io/elsa-core/docs/next/guides/guides-document-approval#first-run

            // The other example P20709AspCoreContentApprovalWorkflowNoUi is working.

            Console.ReadLine();
        }
    }
}

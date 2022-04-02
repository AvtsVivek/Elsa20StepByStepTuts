using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace P20447ContentApprovalPersistenceEfMsSqlWorkerHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var config = builder.Build();

            var elsaSection = config.GetSection("Elsa");

            // I am not sure how to build this.
            // This is created based on the example from the following.
            // https://elsa-workflows.github.io/elsa-core/docs/next/guides/guides-document-approval#first-run

            // The other example P20709AspCoreContentApprovalWorkflowNoUi is working.
            // Ignore this example.

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}

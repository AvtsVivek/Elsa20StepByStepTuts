using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System.Threading.Tasks;

namespace P20180ParentChildWorker
{

    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(
                    (_, services) =>
                    {
                        services
                            .AddElsa(options => options
                                .AddConsoleActivities()
                                .AddWorkflow<ParentWorkflow>()
                                .AddWorkflow<ChildWorkflow>()
                                //.StartWorkflow<ParentWorkflow>()
                                )
                            .AddHostedService<RunParentWorkflowHostedService>()
                            ;
                    });
    }
    // Either use StartWorkflow method or RunParentWorkflowHostedService Worker
    // Commentout one of them.
}
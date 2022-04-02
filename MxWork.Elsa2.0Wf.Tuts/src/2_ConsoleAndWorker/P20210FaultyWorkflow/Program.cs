using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System.Threading.Tasks;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Messages;

namespace P20210FaultyWorkflow
{
    // This is not working at this moment.
    // The consumer is not recieving the message.
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
                                .AddQuartzTemporalActivities()
                                .AddWorkflow<FaultyWorkflow>());
                    });
    }
}
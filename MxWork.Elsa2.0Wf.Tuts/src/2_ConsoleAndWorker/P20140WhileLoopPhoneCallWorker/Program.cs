using P20140WhileLoopPhoneCallWorker.Services;
using P20140WhileLoopPhoneCallWorker.Workflows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P20140WhileLoopPhoneCallWorker.Activities;
using Elsa;

namespace P20140WhileLoopPhoneCallWorker
{

    // This is not currently working. 
    // We need TemporalActivities.
    // But in the current elsa version, 2.0.0-preview7.1545 this does not seem to exist yet. 
    // The timer activity is faulty looks like.
    // Once the AddQuartzTemporalActivities in a futuer build, then remove the line
    // Add timer activity .AddActivity<Timer>()
    // And include the line AddQuartzTemporalActivities()
    // In the PhoneCallWorkflow class, include using Elsa.Activities.Temporal; and remove
    // using Elsa.Activities.Timers; Then uncomment .Timer(Duration.FromSeconds(5))
    // to complete the exercise.
    // 

    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(
                    (_, services) =>
                    {
                        services
                            .AddElsa(options => options//.UseYesSqlPersistence()
                                .AddConsoleActivities()
                                .AddQuartzTemporalActivities()
                                .AddActivity<MakePhoneCall>()
                                .AddWorkflow<PhoneCallWorkflow>())
                            .AddSingleton<PhoneCallService>()
                            .AddHostedService<PhoneCallWorker>();
                    });
    }
}
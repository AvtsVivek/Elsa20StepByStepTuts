using Elsa.Activities.Console;
using Elsa.Activities.Http;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using NodaTime;
using System.Net;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class SimpleSleepingWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            var sleepDurationInSeconds = 5;
            builder
                .WriteLine($"I am going for {sleepDurationInSeconds} second nap. Take care")
                .WriteLine($"The below timer is not triggering. Not clear why")
                //.WriteLine("Can't wait that long? Send me a message at https://localhost:61094/api/wakeup.")
                .Then<Sleep>(sleep => sleep.Set(x => x.Timeout, Duration.FromSeconds(sleepDurationInSeconds)))
                .WriteLine($"Good Morning everyone, had a good sleep for {sleepDurationInSeconds} seconds.")
                .WriteLine("Have a good day.");
        }
    }
}

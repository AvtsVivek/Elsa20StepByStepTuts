using System;
using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class TimerWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            var sleepSeconds = 5;
            builder
                // If I uncomment the following line its not woring. Not sure why
                //.WriteLine($"Wait for {sleepSeconds} seconds and then continue.")
                .Timer(Duration.FromSeconds(sleepSeconds))
                .WriteLine("Hello World")
                .WriteLine(context => $"{context.WorkflowInstance.Id} - Why is this id different on each iteration? Not clear, need to findout..")
                .WriteLine(() => $"Timer event at {DateTime.Now}");
        }
    }
}

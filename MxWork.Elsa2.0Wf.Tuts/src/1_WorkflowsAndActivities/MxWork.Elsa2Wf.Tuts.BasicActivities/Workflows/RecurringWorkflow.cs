using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Models;
using NodaTime;
using System;
using Elsa.Activities.Temporal;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class RecurringWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("Hello World")
                .WriteLine(() => $"Timer event at {DateTime.Now}");
        }
    }
}
// There is something in the Timer activity that makes the workflow kick off.
// If we comment out the timer activity, then the workflow would not start by itself.
// We need a starter or runner in such a clase. 

using System;
using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class FaultyWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartIn(Duration.FromSeconds(1))
                .WriteLine("Catch this!")
                .Then(() => throw new ArithmeticException("Does not compute", new ArgumentException("Incorrect argument", new ArgumentOutOfRangeException("This is the root problem", default(Exception)))));
        }
    }
    // This workflow throws exception. Its not clear how to catch this exception.
}
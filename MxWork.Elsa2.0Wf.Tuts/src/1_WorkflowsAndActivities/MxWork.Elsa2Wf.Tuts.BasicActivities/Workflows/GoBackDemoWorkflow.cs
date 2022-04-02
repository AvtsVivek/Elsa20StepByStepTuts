using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class GoBackDemoWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("Taking a stroll...")
                .If(context => (string?)context.Input == "Brick Wall",
                    @if =>
                    {
                        @if
                            .When(If.False)
                            .WriteLine("Hitting a wall...")
                            // The GoBackDemoActivity is not currently working as it used to be.
                            .Then<GoBackDemoActivity>()
                            .WriteLine("The GoBackDemoActivity is not currently working as it used to be!")
                            .WriteLine("It used to work in 2.0.0.159, but does not in 2.1.0.443");

                        @if
                            .When(If.True)
                            .WriteLine("Going around the wall...")
                            .WriteLine("Made it!");
                    });
        }
    }
}
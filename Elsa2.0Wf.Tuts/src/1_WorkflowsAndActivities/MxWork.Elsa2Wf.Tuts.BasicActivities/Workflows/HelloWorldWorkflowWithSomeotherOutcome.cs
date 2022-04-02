using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class HelloWorldWithSomeotherOutcomeWorkflow : IWorkflow
    {
        public HelloWorldWithSomeotherOutcomeWorkflow()
        {

        }
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<HelloWorldWithSomeotherOutcome>()
                // For the below GoodByeWorld workflow to be executed, 
                // When should be passed, be either "Someother outcome" or "done",
                // If none is passed, then the GoodByeWorld will not be executed.
                // none means implicitly done.
                // https://elsa-workflows.github.io/elsa-core/docs/next/quickstarts/quickstarts-aspnetcore-hello-world
                //.When("Done")
                //
                .When("Someother outcome")
                .Then<GoodByeWorld>();
        }
    }
}
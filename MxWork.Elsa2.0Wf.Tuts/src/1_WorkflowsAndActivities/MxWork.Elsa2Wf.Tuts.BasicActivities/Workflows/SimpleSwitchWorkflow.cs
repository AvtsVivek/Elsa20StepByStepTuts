using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class SimpleSwitchWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("This demonstrates a simple workflow with switch.")
                .WriteLine("Using switch we can branch a workflow.")
                .Then<ForkBranchDecisionActivity>(fork =>
                {
                    fork.When("A")
                        .WriteLine("You are in A branch. First line")
                        .WriteLine("You are in A branch. Second line.")
                        .ThenNamed("Finish");

                    fork.When("B")
                        .WriteLine("You are in B branch. First line")
                        .WriteLine("You are in B branch. Second line.")
                        .ThenNamed("Finish");

                })
                .WriteLine("Workflow finished.")
                .WithName("Finish");
        }
    }
}

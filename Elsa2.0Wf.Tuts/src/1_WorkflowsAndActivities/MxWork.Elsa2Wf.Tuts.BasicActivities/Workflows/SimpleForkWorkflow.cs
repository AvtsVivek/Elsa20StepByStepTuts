using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using NodaTime;
using System;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class SimpleForkWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            //var join = builder.Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny));
            //join.Finish();

            builder
                .WriteLine("This demonistrates a simple workflow with fork .")
                .WriteLine("Using forks we can branch a workflow.")
                .Then<ForkBranchDecisionActivity>()
                .Then<Fork>(
                    fork => fork.WithBranches("A", "B"),
                    fork =>
                    {
                        var aBranch = fork
                            .When("A")
                            .WriteLine("You are in A branch. First line")
                            .WriteLine("You are in A branch. Second line.")
                            .ThenNamed("AfterJoin")
                            ;

                        var bBranch = fork
                            .When("B")
                            .WriteLine("You are in B branch. First line")
                            .WriteLine("You are in B branch. Second line.")
                            .ThenNamed("AfterJoin")
                            ;
                    })
                .WithName("AB Fork")
                .Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny)).WithName("AfterJoin")
                .WriteLine("Workflow finished.")
                .WriteLine("Workflow finished.");
        }

        private void SomeMethod(string textToBeWritten)
        {
            Console.WriteLine(textToBeWritten);
        }
    }
}

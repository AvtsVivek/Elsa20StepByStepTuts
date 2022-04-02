using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class ForkWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<Fork>(fork => fork.WithBranches("A", "B", "C"), fork =>
                {
                    fork
                        .When("A")
                        .While(true, @while => @while
                            .Timer(Duration.FromSeconds(1))
                            .WriteLine("Timer A went off"))
                        .ThenNamed("Join1");

                    fork
                        .When("B")
                        .While(true, @while => @while
                            .Timer(Duration.FromSeconds(5))
                            .WriteLine("Timer B went off"))
                        .ThenNamed("Join1");

                    fork
                        .When("C")
                        .Timer(Duration.FromSeconds(8))
                        .WriteLine("Timer C went off")
                        .ThenNamed("Join1");
                })

                //.Add<Join>(join => join.WithMode(Join.JoinMode.WaitAll)).WithName("Join1")
                //.Add<Join>(join => join.WithMode(Join.JoinMode.WaitAny)).WithName("Join1")
                .Add<Join>().WithName("Join1")
                
                .WriteLine("Container 124567 Joined!")
                .Finish();
        }
    }
}

// All the 3 of the following is giving the same result. Not sure why. Need to findout.
//.Add<Join>(join => join.WithMode(Join.JoinMode.WaitAll)).WithName("Join1")
//.Add<Join>(join => join.WithMode(Join.JoinMode.WaitAny)).WithName("Join1")
//.Add<Join>().WithName("Join1")
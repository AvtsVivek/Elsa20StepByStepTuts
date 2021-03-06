using Elsa.Activities.Console;
using Elsa.Activities.Workflows;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class ParentWorkflow : IWorkflow
    {
        private const long Count = 3;

        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("This is the parent workflow.")
                .WriteLine("Let's kick off the child workflow.")
                .RunWorkflow<ChildWorkflow>(RunWorkflow.RunWorkflowMode.Blocking, Count)
                .WriteLine("Returned back from child workflow.");
        }
    }
}
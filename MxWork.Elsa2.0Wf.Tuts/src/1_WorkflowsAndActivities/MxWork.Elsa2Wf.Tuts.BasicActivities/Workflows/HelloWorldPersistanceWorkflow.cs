using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Models;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class HelloWorldPersistanceWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithPersistenceBehavior(WorkflowPersistenceBehavior.WorkflowBurst)
                .WriteLine("Hello World!");
        }
    }
}

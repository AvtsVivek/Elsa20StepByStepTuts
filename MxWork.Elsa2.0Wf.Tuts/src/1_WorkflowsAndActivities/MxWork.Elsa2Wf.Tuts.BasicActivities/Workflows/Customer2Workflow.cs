using Elsa.Activities.Console;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class Customer2Workflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithCustomAttribute("Customer","Customer2")
                .WriteLine("Specialized workflow for Customer 2");
        }
    }
}
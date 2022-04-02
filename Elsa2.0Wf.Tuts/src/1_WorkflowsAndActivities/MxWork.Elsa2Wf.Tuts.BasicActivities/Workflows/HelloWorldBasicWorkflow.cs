using Elsa.Activities.Console;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class HelloWorldBasicWorkflow : IWorkflow
    {
        public HelloWorldBasicWorkflow()
        {

        }
        public void Build(IWorkflowBuilder builder) => builder
            .WriteLine("Hello World!");
    }
}

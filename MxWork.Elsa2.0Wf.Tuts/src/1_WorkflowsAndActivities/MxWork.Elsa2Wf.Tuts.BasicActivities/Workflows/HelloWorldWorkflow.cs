using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public HelloWorldWorkflow()
        {

        }
        public void Build(IWorkflowBuilder builder)
        {
            // The first activity, here in this case HelloWorld is instanciated twice. 
            // Place a break point in ctor of HelloWorld and run.
            // Not clear why. Need to find out.
            builder
                .StartWith<HelloWorld>()
                .Then<GoodByeWorld>();
        }
    }
}
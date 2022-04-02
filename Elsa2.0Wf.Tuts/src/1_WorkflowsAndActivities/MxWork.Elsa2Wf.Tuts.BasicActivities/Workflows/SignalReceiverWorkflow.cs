using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class SignalReceiverWorkflow : IWorkflow
    {   
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .SignalReceived("Demo Signal")
                .WriteLine(context => $"Signal received!");
        }
    }
}
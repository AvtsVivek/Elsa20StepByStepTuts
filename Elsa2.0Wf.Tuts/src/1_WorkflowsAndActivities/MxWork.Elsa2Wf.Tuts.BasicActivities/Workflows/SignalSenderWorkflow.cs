using Elsa.Activities.Console;
using Elsa.Activities.Signaling;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class SignalSenderWorkflow : IWorkflow
    {   
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine(context => $"Sending signal from workflow!")
                .SendSignal("Demo Signal")
                .WriteLine(context => $"Signal sent from workflow!");
        }
    }
}
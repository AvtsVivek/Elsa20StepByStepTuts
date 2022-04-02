using Elsa.Activities.Console;
using Elsa.Activities.Signaling;
using Elsa.Builders;

namespace P20240SignalingConsole
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
// This file cannot be moved to the MxWork.Elsa2Wf.Tuts.BasicActivities project.
// Moving this is creating problem.
// In program.cs file, when the following lines are executed, something is happenidng(not clear what)
// and exceptions are being thrown. So its better to keep these here itself.
// var startupRunner = services.GetRequiredService<IStartupRunner>();
// await startupRunner.StartupAsync();
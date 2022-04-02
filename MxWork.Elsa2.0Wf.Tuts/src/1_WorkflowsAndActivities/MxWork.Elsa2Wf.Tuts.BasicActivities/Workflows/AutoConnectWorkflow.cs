using Elsa.Activities.Console;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    /// <summary>
    /// A basic workflow with just one WriteLine activity.
    /// </summary>
    public class AutoConnectWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder) => builder
            .WriteLine("Running demo workflow.")
            .Then<AutoConnectActivity>() // Even though this activity returns "Next", we still want to execute the next activity (which is connected via "Done").
            .WriteLine("Done!");
    }
}
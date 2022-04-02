using Elsa.Activities.Console;
using Elsa.Activities.Primitives;
using Elsa.Activities.Rebus;
using Elsa.Activities.Workflows;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Messages;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    /// <summary>
    /// Listen for new OrderReceived messages and kick off customer-specific child workflows. 
    /// </summary>
    public class OrderReceivedWorkflow : IWorkflow
    {

        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<RebusMessageReceived>(activity => activity.Set(x => x.MessageType, typeof(OrderReceived)))
                .SetVariable(context => context.GetInput<OrderReceived>())
                .WriteLine(context => $"Received a new order for {context.GetVariable<OrderReceived>()!.CustomerId}.")
                .RunWorkflow("ProcessOrderWorkflow", context => context.GetVariable<OrderReceived>()!.CustomerId, RunWorkflow.RunWorkflowMode.Blocking)
                .WriteLine("Returned back from child workflow.");
        }
    }
}
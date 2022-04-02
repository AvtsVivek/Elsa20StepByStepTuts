using Elsa.Activities.Console;
using Elsa.Activities.Primitives;
using Elsa.Activities.Rebus;
using Elsa.Activities.Workflows;
using Elsa.Builders;
using Elsa.Models;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Messages;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    /// <summary>
    /// Listen for new OrderReceived messages and kick off customer-specific child workflows. 
    /// </summary>
    public class OrderReceivedRunChildWithCustomAttributesWorkflow : IWorkflow
    {

        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<RebusMessageReceived>(activity => activity.Set(x => x.MessageType, typeof(OrderReceived)))
                .SetVariable(context => context.GetInput<OrderReceived>())
                .WriteLine(context => $"Received a new order for {context.GetVariable<OrderReceived>()!.CustomerId}.")
                .RunWorkflow(activity => activity.WithCustomAttributes(context => new Variables().Set("Customer", context.GetVariable<OrderReceived>()!.CustomerId)))
                .WriteLine("Returned back from child workflow.");
        }
    }
}
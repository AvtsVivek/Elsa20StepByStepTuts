﻿using Elsa.Activities.Console;
using Elsa.Activities.Rebus;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Messages;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class ConsumerWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<RebusMessageReceived>(messageReceived => messageReceived.Set(x => x.MessageType, typeof(Greeting)))
                .WriteLine(context =>
                {
                    var greeting = context.GetInput<Greeting>()!;
                    return $"Received a greeting from {greeting.From}, saying \"{greeting.Message}\" to {greeting.To}!";
                });
        }
    }
}
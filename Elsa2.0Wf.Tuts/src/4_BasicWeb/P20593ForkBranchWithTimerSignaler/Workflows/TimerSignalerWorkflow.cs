using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.Http;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Primitives;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;
using NodaTime;
using System.Net;

namespace P20593ForkBranchWithTimerSignaler.Workflows
{
    public class TimerSignalerWorkflow : IWorkflow
    {
        private readonly IClock _clock;
        private readonly Duration _timeOut;

        public TimerSignalerWorkflow(IClock clock)
        {
            _clock = clock;
            _timeOut = Duration.FromSeconds(10);
        }

        public void Build(IWorkflowBuilder builder)
        {
            builder
                .HttpEndpoint(activity => activity.WithPath("/StartTimerSignalerWorkflow").WithMethod(HttpMethods.Get))
                .SetVariable("SignalUrl", context => context.GenerateSignalUrl("hurry"))
                .WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                //.WithContentType("json")
                .WithContent(context =>
                
                $"{{\"message\":\"The demo completes in {_timeOut.ToString()} " +
                $"({_clock.GetCurrentInstant().Plus(_timeOut)}). " +
                $"Can't wait that long? Send me the secret 'hurry' signal!\", " +
                $"\"signalUrl\":\"{context.GetVariable<string>("SignalUrl")}\", " +
                $"\"totalTimeout\":\"{_timeOut.ToString()}\", " +
                $"\"timeoutDays\":\"{_timeOut.Days}\", " +
                $"\"timeoutHours\":\"{_timeOut.Hours}\", " +
                $"\"timeoutMinutes\":\"{_timeOut.Minutes}\", " +
                $"\"timeoutSeconds\":\"{_timeOut.Seconds}\"" +
                $"}}"

                ))

                .Then<Fork>(
                    fork => fork.WithBranches("Timer", "Signal"),
                    fork =>
                    {
                        fork
                            .When("Timer")
                            .Timer(_timeOut)
                            .SetVariable("CompletedVia", "Timer")
                            .ThenNamed("Join");

                        fork
                            .When("Signal")
                            .SignalReceived("hurry")
                            .SetVariable("CompletedVia", "Signal")
                            .ThenNamed("Join");
                    })
                .Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny)).WithName("Join")
                .WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(context =>
                $"Demo {GetCorrelationId(context)} completed successfully via " +
                $"{context.GetVariable<string>("CompletedVia")}!"));
                //.WriteLine(context => $"Demo {GetCorrelationId(context)} completed successfully via {context.GetVariable<string>("CompletedVia")}!");
        }

        private string GetCorrelationId(ActivityExecutionContext context) => context.WorkflowExecutionContext.CorrelationId;
    }
}
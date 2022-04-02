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

namespace P20596ForkBranchWithTimerAndHttp.Workflows
{
    public class TimerHttpWorkflow : IWorkflow
    {
        private readonly IClock _clock;
        private readonly Duration _timeOut;

        public TimerHttpWorkflow(IClock clock)
        {
            _clock = clock;
            _timeOut = Duration.FromSeconds(5);
        }

        public void Build(IWorkflowBuilder builder)
        {
            builder
                .HttpEndpoint(activity => activity.WithPath("/StartTimerHttpWorkflow").WithMethod(HttpMethods.Get))
                .SetVariable("SignalUrl", context => context.GenerateSignalUrl("hurry"))
                .WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                //.WithContentType("json")
                .WithContent(context =>
                
                $"{{\"message\":\"The demo completes in {_timeOut.ToString()} " +
                $"({_clock.GetCurrentInstant().Plus(_timeOut)}). " +
                $"Can't wait that long? Send a http get request to ResumeTimerHttpWorkflow!\", " +
                //Send a http get request to ResumeTimerHttpWorkflow to finish the workflow.
                //$"\"signalUrl\":\"{context.GetVariable<string>("SignalUrl")}\", " +
                $"\"ResumeTimerHttpWorkflow\":\"ResumeTimerHttpWorkflow\", " +
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
                            .HttpEndpoint(activity => activity.WithPath("/ResumeTimerHttpWorkflow").WithMethod(HttpMethods.Get))
                            //.SignalReceived("hurry")
                            .SetVariable("CompletedVia", "Http")
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
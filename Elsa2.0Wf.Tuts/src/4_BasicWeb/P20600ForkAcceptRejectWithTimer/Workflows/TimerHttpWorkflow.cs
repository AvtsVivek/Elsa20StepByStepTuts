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
            _timeOut = Duration.FromSeconds(15);
        }

        public void Build(IWorkflowBuilder builder)
        {
            builder
                .HttpEndpoint(activity => activity.WithPath("/StartTimerHttpWorkflow").WithMethod(HttpMethods.Get))
                //.SetVariable("SignalUrl", context => context.GenerateSignalUrl("hurry"))
                .WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                //.WithContentType("json")
                .WithContent(context =>
                
                $"{{\"message\":\"The document will be auto rejected in {_timeOut.ToString()} " +
                $"({_clock.GetCurrentInstant().Plus(_timeOut)}). " +
                $"To approve or reject before that, send a http get request to approve or reject!\", " +
                //Send a http get request to ResumeTimerHttpWorkflow to finish the workflow.
                //$"\"signalUrl\":\"{context.GetVariable<string>("SignalUrl")}\", " +
                $"\"ApproveUrl\":\"Approve\", " +
                $"\"RejectUrl\":\"Reject\", " +
                $"\"totalTimeout\":\"{_timeOut.ToString()}\", " +
                $"\"timeoutDays\":\"{_timeOut.Days}\", " +
                $"\"timeoutHours\":\"{_timeOut.Hours}\", " +
                $"\"timeoutMinutes\":\"{_timeOut.Minutes}\", " +
                $"\"timeoutSeconds\":\"{_timeOut.Seconds}\"" +
                $"}}"

                ))

                .Then<Fork>(
                    fork => fork.WithBranches("Timer", "Approve", "Reject"),
                    fork =>
                    {
                        fork
                            .When("Timer")
                            .Timer(_timeOut)
                            .SetVariable("CompletedVia", "Timer")
                            .SetVariable("ApprovalRejectionMessage", $"Document rejected automatically because of timeout.")
                            .WriteLine("Document rejected automatically because of timeout.")
                            .ThenNamed("Join");

                        fork
                            .When("Approve")
                            .HttpEndpoint(activity => activity.WithPath("/Approve").WithMethod(HttpMethods.Get))
                            .SetVariable("CompletedVia", "Http")
                            .SetVariable("ApprovalRejectionMessage", $"Document received and Approved.")
                            .ThenNamed("Join");

                        fork
                            .When("Approve")
                            .HttpEndpoint(activity => activity.WithPath("/Reject").WithMethod(HttpMethods.Get))
                            .SetVariable("CompletedVia", "Http")
                            .SetVariable("ApprovalRejectionMessage", $"Document received but Rejected.")
                            .ThenNamed("Join");
                    })
                .WithName("Accept Reject Fork")
                .Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny)).WithName("Join")
                .WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(context =>
                $"Document completed successfully. " +
                $"{context.GetVariable<string>("ApprovalRejectionMessage")}!"

                ));
        }
    }
}
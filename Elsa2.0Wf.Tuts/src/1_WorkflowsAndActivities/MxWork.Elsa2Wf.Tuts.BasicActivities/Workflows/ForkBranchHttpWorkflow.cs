using System;
using System.Net;
using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.Http;
using Elsa.Activities.Http.Models;
using Elsa.Builders;
using Microsoft.AspNetCore.Http;
using Elsa.Activities.Primitives;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class ForkBranchHttpWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            // Demonstrating that we can create activities and connect to them later on by using the activity builder reference.
            //var join = builder.Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny));
            //join.Finish();

            builder
                .HttpEndpoint(activity => activity.WithPath("/documents/start").WithMethod(HttpMethods.Get))

                .WriteHttpResponse(
                    activity => activity
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithContentType("text/html")
                        .WithContent(context => $"Document received with ID! Awaiting Approve or Reject response."))

                .Then<Fork>(
                    fork => fork.WithBranches("Approve", "Reject"),
                    fork =>
                    {
                        var approveBranch = fork
                            .When("Approve")
                            .HttpEndpoint(activity => activity.WithPath("/documents/approve").WithMethod(HttpMethods.Get))
                            //.WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                            //.WithContentType("text/html")
                            //.WithContent(context => $"Document received and Approved."))
                            .SetVariable("ApprovalRejectionMessage", $"Document received and Approved.")
                            .ThenNamed("AfterJoin");


                        var rejectBranch = fork
                            .When("Reject")
                            .HttpEndpoint(activity => activity.WithPath("/documents/reject").WithMethod(HttpMethods.Get))
                            //.WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                            //.WithContentType("text/html")
                            //.WithContent(context => $"Document received but rejected."))
                            .SetVariable("ApprovalRejectionMessage", $"Document received but rejected.")
                            .ThenNamed("AfterJoin");

                    })
                .WithName("Accept Reject Fork")
                .Add<Join>(x => x.WithMode(Join.JoinMode.WaitAny)).WithName("AfterJoin")
                //.WriteLine("Workflow finished.")
                .WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(context => context.GetVariable("ApprovalRejectionMessage")!.ToString() + " Workflow completed."))
                ;
        }
    }
}

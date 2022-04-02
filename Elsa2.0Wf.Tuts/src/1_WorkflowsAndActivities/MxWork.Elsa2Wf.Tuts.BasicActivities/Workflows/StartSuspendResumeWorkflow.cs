using Elsa.Activities.Http;
using Elsa.Builders;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class StartSuspendResumeWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
            // Configure a Receive HTTP Request trigger that executes on incoming HTTP POST requests.
            .HttpEndpoint(activity => activity.WithPath("/StartWorkflow").WithMethod(HttpMethods.Get))

            //// Store the registration as a workflow variable for easier access.
            //.SetVariable(context => (Registration)((HttpRequestModel)(context.Input))?.Body)

            // Correlate the workflow by email address.
            //.Correlate(context => context.GetVariable<Registration>()!.Email)

            // Write an HTTP response with a hyperlink to continue the workflow (notice the presence of the "correlation" query string parameter). 
            .WriteHttpResponse(activity => activity
                .WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(context =>
            {
                //var registration = context.GetVariable<StartData>()!;
                var workflowInstanceId = context.WorkflowInstance.Id;
                // Not able to clearly understand what first pass is.
                // 
                var isFirstPass = context.WorkflowExecutionContext.IsFirstPass;

                return $"Thanks for starting the workflow. Now this workflow will be suspended.! " +
                $"Please <a href=\"/resume\">click here to resume the workflow</a>. " +
                $"The workflow instance id is {workflowInstanceId}. " +
                $"Its current status is {context.WorkflowInstance.WorkflowStatus}. " +
                $"First pass value is {isFirstPass}";
            }))

            // HttpEndPoint is a blocking activity. To know more about them, look at this link
            // https://elsa-workflows.github.io/elsa-core/docs/next/guides/guides-blocking-activities#blocking-activities
            // Configure another Receive HTTP Request trigger that executes on incoming HTTP GET requests.
            // This will cause the workflow to become suspended and executed once the request comes in.
            .HttpEndpoint(activity => activity.WithPath("/resume").WithMethod(HttpMethods.Get))

            // Write an HTTP response with a thank-you note.
            // Notice that the correct workflow instance is resumed base on the incoming correlation ID.
            .WriteHttpResponse(activity => activity
                .WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(
                    context =>
                    {
                        var workflowInstanceId = context.WorkflowInstance.Id;
                        var isFirstPass = context.WorkflowExecutionContext.IsFirstPass;

                        return $"Thanks!! Workflow with id {workflowInstanceId} has resumed. " +
                        $"Its current status is {context.WorkflowInstance.WorkflowStatus}. " +
                        $"Now it will end. " +
                        $"First pass value is {isFirstPass}";
                    }));

        }
    }
}

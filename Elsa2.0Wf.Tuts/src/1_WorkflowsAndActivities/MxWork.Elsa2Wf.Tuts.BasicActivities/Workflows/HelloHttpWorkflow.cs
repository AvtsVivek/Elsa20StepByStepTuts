using System.Net;
using Elsa.Activities.Http;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    /// <summary>
    /// A workflow that is triggered when HTTP requests are made to /hello and writes a response.
    /// </summary>
    public class HelloHttpWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<HttpEndpoint>(httpEndpoint => httpEndpoint
                    .WithPath("/hello")
                    .WithMethod("GET")
                )
                .WriteHttpResponse(HttpStatusCode.OK, "Hello World from Workflow!", "text/html");
        }
    }
}

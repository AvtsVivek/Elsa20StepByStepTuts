using System.Net;
using Elsa.Activities.Http;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class RocketWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteHttpResponse(activity => activity
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithContentType("text/plain")
                    .WithContent("We're going to the moon!"));
        }
    }
}

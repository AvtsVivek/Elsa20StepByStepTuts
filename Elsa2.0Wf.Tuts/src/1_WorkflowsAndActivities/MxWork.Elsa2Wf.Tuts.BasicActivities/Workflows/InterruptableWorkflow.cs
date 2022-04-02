using Elsa.Activities.Http;
using Elsa.Builders;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using NodaTime;
using System.Net;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class InterruptableWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                //.WriteLine("This workflow will sleep for 5 minutes before it continues.")
                //.WriteLine("Can't wait that long? Send me a message at https://localhost:61094/api/wakeup.")
                //.Then<Sleep>(sleep => sleep.Set(x => x.Timeout, Duration.FromMinutes(5)))
                //.WriteLine("Done.")


                .WriteHttpResponse(activity => activity
                .WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(context =>
                {
                    var workflowId = context.WorkflowInstance.Id;
                    var typeName = this.GetType().Name;
                    var returnString = $"Thanks for starting the workflow. Now the workflow will be suspended.! " +
                    $"Please <a href=\"/api/wakeup/Wakeup/" + typeName + "/" + workflowId + 
                    $"\" >click here to resume this specific workflow</a>";
                    return returnString;
                }))
                .Then<Sleep>(sleep => sleep.Set(x => x.Timeout, Duration.FromMinutes(5)))
                                .WriteHttpResponse(activity => activity
                .WithStatusCode(HttpStatusCode.OK)
                .WithContentType("text/html")
                .WithContent(context =>
                {
                    return "Done";
                }));
        }
    }
}

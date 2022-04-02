//using Elsa.Activities.Console;
//using Elsa.Activities.Http;
//using Elsa.Activities.Temporal;
//using Elsa.Builders;
//using Microsoft.AspNetCore.Http;
//using NodaTime;
//using System.Net;

//namespace P20650ApiEndpoints
//{
//    public class HeartbeatWorkflow : IWorkflow
//    {
//        private readonly IClock _clock;
//        public HeartbeatWorkflow(IClock clock) => _clock = clock;

//        public void Build(IWorkflowBuilder builder) =>
//            builder
//                //.WriteLine("Here is hearbeat workflow. The following timer activity, if it is the first one, then it starts the workflow automatically, without the wroflow being explicitly started.")
//                //.HttpEndpoint(activity => activity.WithPath("/StartHeartbeatWorkflow").WithMethod(HttpMethods.Get))
//                .Timer(Duration.FromSeconds(10))
//                .WriteLine(context => $"Heartbeat at {_clock.GetCurrentInstant()} and id is {context.WorkflowInstance.Id} Why is this id different on each iteration? Not clear, need to findout..")
//                //.WriteLine(context => $"{context.WorkflowInstance.Id} - Why is this id different on each iteration? Not clear, need to findout..")
//            //.WriteHttpResponse(activity => activity.WithStatusCode(HttpStatusCode.OK)
//            //.WithContentType("text/html")
//            //.WithContent(context =>
//            //$"Document completed successfully. "
//            //))
//            ;
//    }
//}

using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.CustomActivityLibrary.Model;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;
using System.Collections.Generic;

namespace Elsa.CustomActivityLibrary.Activities
{
    [Trigger(
        Category = "Elsa Custom Activities",
        Description = "Triggers when a file is received"
    )]
    //FileReceived1
    public class FileReceived : Activity
    {

        // This ActivityProperty has to be changed to ActivityOutput attribute.

        //[ActivityProperty] public FileModel Output { get; set; }

        // The following is not currently not supported. Only after up[grading to 2.1 we will look into this.
        // https://elsa-workflows.github.io/elsa-core/docs/next/guides/guides-blocking-activities#file-extension-filter
        //// ActivityInput
        //[ActivityProperty(
        //    Hint = "Specify a list of file extensions to filter. LEave empty to allow any file extension.",
        //    UIHint = "multi-text",
        //    DefaultSyntax = SyntaxNames.Json,
        //    DefaultValue = new string[0],
        //    SupportedSyntaxes = new[] { SyntaxNames.Json, SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        //public ICollection<string> SupportedFileExtensions { get; set; } = new List<string>();

        public string ReceivedFileName { get; set; }


        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            // Not clear what this first pass is. 
            // https://github.com/elsa-workflows/elsa-core/blob/5ec2b8dd022f3df43554f4467762d2d8fc581ca5/src/activities/Elsa.Activities.Http/Activities/HttpEndpoint/HttpEndpoint.cs#L62
            // https://elsa-workflows.github.io/elsa-core/docs/next/guides/guides-blocking-activities#suspend--resume
            // When the activity is used as a starting activity, it will return "Done" and execution of the workflow will continue.
            // But when the activity is used as a blocking activity (i.e. not as the first activity of the workflow), the activity will suspend the workflow.
            // So I guess, first pass means the the very first activity.
            // 
            //return context.WorkflowExecutionContext.IsFirstPass ? Done() : Suspend();
            return context.WorkflowExecutionContext.IsFirstPass ? OnExecuteInternal(context) : Suspend();

        }

        protected override IActivityExecutionResult OnResume(ActivityExecutionContext context)
        {
            //return Done();
            return OnExecuteInternal(context);
        }

        private IActivityExecutionResult OnExecuteInternal(ActivityExecutionContext context)
        {
            //HttpRequestModel input = context.GetInput<HttpRequestModel>();
            //return this.Done(input);

            var input = context.GetInput<FileModel>();
            //ReceivedFileName = fileReceived.FileName;
            //Output(fileReceived);

            return Done(input);
        }
    }
}

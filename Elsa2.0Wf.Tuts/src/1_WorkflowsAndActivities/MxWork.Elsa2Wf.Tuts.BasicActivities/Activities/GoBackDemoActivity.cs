using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    /// <summary>
    /// A custom activity that instructs the workflow runner to go back one step under certain conditions.
    /// </summary>
    public class GoBackDemoActivity : Activity
    {
        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            // this is takeing from here.
            // https://github.com/elsa-workflows/elsa-core/tree/feature/elsa-2.0/src/samples/console/Elsa.Samples.GoBackConsole
            // This has bug in the latest version of the nuget package. 
            // Elsa" Version="2.0.0-preview7.1545"
            // The latest source code does not. It works fine.
            // Tell the workflow that it hit a brick wall.
            //return new GoBackResult("Brick Wall");
            // The GoBackResult is not currently working
            return Done();
        }
    }
}
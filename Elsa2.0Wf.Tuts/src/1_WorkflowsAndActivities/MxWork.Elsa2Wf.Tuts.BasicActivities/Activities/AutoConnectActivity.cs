using System;
using Elsa;
using Elsa.ActivityResults;
using Elsa.Services;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    // The important aspect of this activity is that it returns an outcome other than "Done".
    // Despite that, we still want to be able to execute 
    // any activities connected to this one if that connection is established with the "Done" outcome, 
    // because that's what the Workflow Builder API uses when connecting activities implicitly.

    // Its not clear what the above statement says. 
    // Tried with OutcomeNames.Done. Still the result is the same.
    public class AutoConnectActivity : Activity
    {
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine("Executing custom activity.");
            //return Outcome("Next");
            return Outcome(OutcomeNames.Next);
            //return Outcome(OutcomeNames.Done);
        }
    }
}
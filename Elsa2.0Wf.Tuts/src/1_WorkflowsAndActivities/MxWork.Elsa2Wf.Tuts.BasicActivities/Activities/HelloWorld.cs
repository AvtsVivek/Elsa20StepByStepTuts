using System;
using Elsa;
using Elsa.ActivityResults;
using Elsa.Services;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    public class HelloWorld : Activity
    {
        private static int _counter;
        public HelloWorld()
        {
            _counter++;
        }
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine("Hello world! " + _counter);
            // The following two are same. See the followng code.
            // https://github.com/elsa-workflows/elsa-core/blob/master/src/core/Elsa.Abstractions/Services/Workflows/Activity.cs
            // var done = Done();
            var done2 = Outcome(OutcomeNames.Done);
            return done2;
        }
    }
}
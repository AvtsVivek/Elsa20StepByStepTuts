using System;
using Elsa.ActivityResults;
using Elsa.Services;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    public class GoodByeWorld : Activity
    {
        private static int _counter;
        public GoodByeWorld()
        {
            _counter++;
        }

        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine("Goodbye world... " + _counter);
            var done = Done();
            return done;
        }

    }
}
using System;
using Elsa;
using Elsa.ActivityResults;
using Elsa.Services;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    public class HelloWorldWithSomeotherOutcome : Activity
    {
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine("Hello world with someother outcome" );
            Console.WriteLine("The outcome here is \"Someother outcome\"");
            Console.WriteLine("If the next activity is to execute, then there should be a When method call between the two ");
            Console.WriteLine("as follows.");
            Console.WriteLine(".When(\"Someother outcome\")");
            Console.WriteLine("Also done outcome is implicit.");
            Console.WriteLine("That is, .When(\"Done\") is also fine.");
            //https://elsa-workflows.github.io/elsa-core/docs/next/quickstarts/quickstarts-aspnetcore-hello-world
            var done2 = Outcome("Someother outcome");
            return done2;
        }
    }
}

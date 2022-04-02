using System;
using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;
using Elsa.Services.Models;
using Elsa.Activities.Primitives;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class SimpleWhileWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("Type y to continue the loop ")
                .WriteLine("Any other key will exit the loop")
                .WriteLine("Lets go...")
                .ReadLine()
                .Then(context => SetCurrentCount(context, context.Input!.ToString()!))
                .While(context => IsYes(context), iteration => iteration
                    .WriteLine(context => "You typed: " + GetCurrentCount(context))
                    .WriteLine("So loop continues. Type anything other than y or Y to stop and exit the program.")
                    .WriteLine("Try again")
                    .ReadLine()
                    .Then(context => SetCurrentCount(context, context.Input!.ToString()!)))
                .WriteLine(context => "You typed: " + GetCurrentCount(context))
                .WriteLine("So ending.")
                .Finish();
        }

        private static bool IsYes(ActivityExecutionContext context)
        {
            var userInput = context.GetVariable<string>("UserInput")!;

            if (userInput.ToLowerInvariant() == "y")
                return true;
            return false;
        }
        private static string GetCurrentCount(ActivityExecutionContext context)
        {
            var userInput = context.GetVariable<string>("UserInput")!;
            return userInput; 
        }
        private static void SetCurrentCount(ActivityExecutionContext context, string value)
        {
            context.SetVariable("UserInput", value);
        }
    }
}
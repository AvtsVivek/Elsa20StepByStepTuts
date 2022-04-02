using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;
using Elsa.Services.Models;
using System;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class IfControlFlowDemoWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("Type a number")
                .ReadLine()
                .Then(context => SetUserInputValue(context, context.Input!.ToString()!))
                .If(EvaluateForIf,
                    @if =>
                    {
                        @if
                            .When(If.False)
                            .WriteLine("You did not type a number");
                        @if
                            .When(If.True)
                            .WriteLine("Yes, you typed an integer")
                            .WriteLine("Made it!");
                    });
        }

        private bool EvaluateForIf(ActivityExecutionContext activityExecutionContext)
        {
            var userInputString = GetUserInputValue(activityExecutionContext);
            int userIntInput;
            return int.TryParse(userInputString, result: out userIntInput);
        }
        private string GetUserInputValue(ActivityExecutionContext context)
        {
            var userInput = context.GetVariable<string>("UserInput")!;
            return userInput;
        }
        private void SetUserInputValue(ActivityExecutionContext context, string value)
        {
            context.SetVariable("UserInput", value);
        }
    }
}
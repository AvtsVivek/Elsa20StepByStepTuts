using Elsa.Activities.Console;
using Elsa.Builders;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.UserTask.Activities;
using System.Collections.Generic;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class UserTaskWorkflow : IWorkflow
    {

        public void Build(IWorkflowBuilder workflow)
        {
            workflow
                .WriteLine("Workflow started. Waiting for user action.").WithName("Start")
                .Then<UserTask>(
                    activity => activity.Set(x => x.Actions, new[] { "Accept", "Reject", "Needs Work" }),
                    //activity => activity.Set(x => x.Actions, _availableActions.ToArray() ),
                    userTask =>
                    {
                        userTask
                            .When("Accept")
                            .WriteLine("Great! Your work has been accepted.")
                            //.Then<Finish>();
                        .ThenNamed("Exit");

                        userTask
                            .When("Reject")
                            .WriteLine("Sorry! Your work has been rejected.")
                            //.Then<Finish>();
                        .ThenNamed("Exit");

                        userTask
                            .When("Needs Work")
                            .WriteLine("So close! Your work needs a little bit more work.")
                            //.Then<Finish>();
                        .ThenNamed("Exit");
                    }
                )
                .WriteLine("Workflow finished.").WithName("Exit");
        }
    }
}
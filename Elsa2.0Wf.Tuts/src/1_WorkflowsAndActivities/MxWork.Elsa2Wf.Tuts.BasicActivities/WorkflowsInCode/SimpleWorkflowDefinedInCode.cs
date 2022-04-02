using System;
using System.Collections.Generic;
using Elsa;
using Elsa.Activities.Console;
using Elsa.Models;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.WorkflowsInCode
{
    public class SimpleWorkflowDefinedInCode
    {
        public WorkflowDefinition GetWorkflowDefWithTwoActivities()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString("N"),
                DefinitionId = Guid.NewGuid().ToString("N"),
                Name = "ProcessOrderWorkflow",
                DisplayName = "Process Order Workflow",
                Description = "Process new orders",
                Version = 1,
                IsPublished = true,
                ContextOptions = new WorkflowContextOptions
                {
                    ContextFidelity = WorkflowContextFidelity.Burst,
                    ContextType = typeof(string)
                },
                Activities = new[]
                {
                    new ActivityDefinition
                    {
                        ActivityId = "activity-1",
                        Description = "Write \"Hello\"",
                        Type = "WriteLine",
                        Name = "Activity1",
                        DisplayName = "Write \"Hello\"",
                        Properties = new List<ActivityDefinitionProperty>()
                        {
                            ActivityDefinitionProperty.Literal("Text", "Hello")
                        }
                    },
                    new ActivityDefinition
                    {
                        ActivityId = "activity-2",
                        Description = "Write \"World!\"",
                        Type = "WriteLine",
                        Name = "Activity2",
                        DisplayName = "Write \"World!\"",
                        Properties = new List<ActivityDefinitionProperty>()
                        {
                            ActivityDefinitionProperty.Literal("Text", "World!")
                        }
                    }
                },
                Connections = new[] { new ConnectionDefinition("activity-1", "activity-2", OutcomeNames.Done) }
            };
        }

        public WorkflowDefinition GetWorkflowDef()
        {
            var writeLine = new ActivityDefinition
            {
                ActivityId = "writeline-activity",
                Type = nameof(WriteLine),
                Properties = new List<ActivityDefinitionProperty>()
                {
                    ActivityDefinitionProperty.Literal(nameof(WriteLine.Text), "Hello World!")
                }
            };

            var readLine = new ActivityDefinition
            {
                ActivityId = "readline-activity",
                Type = nameof(ReadLine)
            };

            var activities = new[] { writeLine, readLine };

            var connections = new[] {
                new ConnectionDefinition(
                    writeLine.ActivityId,
                    readLine.ActivityId,
                    OutcomeNames.Done)
            };

            return new()
            {
                Activities = activities,
                Connections = connections
            };
        }
    }
}

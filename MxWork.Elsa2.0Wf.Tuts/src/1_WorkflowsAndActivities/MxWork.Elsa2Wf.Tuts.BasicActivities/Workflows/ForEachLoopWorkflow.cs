using System.Globalization;
using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    /// <summary>
    /// This workflow demonistrates foreach 
    /// </summary>
    public class ForEachLoopWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WriteLine("Enumerating all months of the year:")
                .ForEach(DateTimeFormatInfo.CurrentInfo!.MonthNames, iterate => iterate.WriteLine(context =>
                {
                    var scope = context.ForEachScope<string>();
                    return $"{scope.CurrentIndex + 1}. {scope.CurrentValue}";
                }))
                .WriteLine("Done.");
        }
    }
}
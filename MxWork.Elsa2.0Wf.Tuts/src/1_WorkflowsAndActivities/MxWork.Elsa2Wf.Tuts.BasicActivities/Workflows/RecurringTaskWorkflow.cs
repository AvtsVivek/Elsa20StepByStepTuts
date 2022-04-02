using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows
{
    public class RecurringTaskWorkflow : IWorkflow
    {
        private readonly IClock _clock;

        public RecurringTaskWorkflow(IClock clock) => _clock = clock;

        public void Build(IWorkflowBuilder builder) =>
            builder
                .Timer(Duration.FromSeconds(2))
                .WriteLine(() => $"It's now {_clock.GetCurrentInstant()}. Let's do this thing!");
    }
}

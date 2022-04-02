using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20180ParentChildWorker
{
    public class RunParentWorkflowHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public RunParentWorkflowHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var workflowRunner = scope.ServiceProvider.GetRequiredService<IBuildsAndStartsWorkflow>();
            await workflowRunner.BuildAndStartWorkflowAsync<ParentWorkflow>(cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

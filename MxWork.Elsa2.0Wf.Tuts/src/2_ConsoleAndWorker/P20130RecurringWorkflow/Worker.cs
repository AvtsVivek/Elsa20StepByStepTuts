using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20130RecurringWorkflow
{
    /// <summary>
    /// A simple worker that simulates a progressing phone call.
    /// </summary>
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            // Get a workflow starter.
            var workflowStarter = scope.ServiceProvider.GetRequiredService<IBuildsAndStartsWorkflow>();

            await workflowStarter.BuildAndStartWorkflowAsync<HelloWorldBasicWorkflow>(cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
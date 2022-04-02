using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P20140WhileLoopPhoneCallWorker.Workflows;

namespace P20140WhileLoopPhoneCallWorker.Services
{
    /// <summary>
    /// A simple worker that simulates a progressing phone call.
    /// </summary>
    public class PhoneCallWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public PhoneCallWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            //var workflowRunner = scope.ServiceProvider.GetRequiredService<IBuildsAndStartsWorkflow>();
            // Get a workflow runner.
            //var workflowRunner = scope.ServiceProvider.GetRequiredService<IWorkflowRunner>();

            // Get a workflow starter.
            var workflowStarter = scope.ServiceProvider.GetRequiredService<IBuildsAndStartsWorkflow>();

            // Execute the workflow.
            //await workflowStarter.BuildAndStartWorkflowAsync<AutoConnectWorkflow>();

            await workflowStarter.BuildAndStartWorkflowAsync<PhoneCallWorkflow>(cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
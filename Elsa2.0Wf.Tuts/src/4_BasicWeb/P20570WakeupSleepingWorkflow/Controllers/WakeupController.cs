using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Persistence;
//using Elsa.Persistence.Specifications.WorkflowDefinitions;
using Elsa.Persistence.Specifications.WorkflowInstances;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Mvc;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20570WakeupSleepingWorkflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeupController : Controller
    {
        private readonly IWorkflowTriggerInterruptor _workflowInterruptor;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;
        public WakeupController(IWorkflowTriggerInterruptor workflowInterruptor,
            IWorkflowInstanceStore workflowInstanceStore
            )
        {
            _workflowInterruptor = workflowInterruptor;
            _workflowInstanceStore = workflowInstanceStore;
        }

        [HttpGet]
        public async Task<IActionResult> Handle(CancellationToken cancellationToken)
        {
            // Interrupt each workflow by triggering the "Sleep" activity.
            var workflowInstances = (await _workflowInterruptor.InterruptActivityTypeAsync(nameof(Sleep), 
                cancellationToken: cancellationToken)).ToList();
            return Ok($"Interrupted {workflowInstances.Count} workflows.");
        }

        [HttpGet("WakeupAll")]

        public async Task<IActionResult> WakeupAll(CancellationToken cancellationToken)
        {
            // Interrupt each workflow by triggering the "Sleep" activity.
            var workflowInstances = (await _workflowInterruptor.InterruptActivityTypeAsync(nameof(Sleep),
                cancellationToken: cancellationToken)).ToList();
            return Ok($"Interrupted {workflowInstances.Count} workflows.");
        }

        [HttpGet("Wakeup/{specId}/{id}")]
        public async Task<IActionResult> Wakeup(string specId, string id, CancellationToken cancellationToken)
        {
            var workflowDefIdSpec = GetWorkflowDefIdSpec(specId);

            var count = await _workflowInstanceStore.CountAsync(workflowDefIdSpec);

            var wfInstances = await _workflowInstanceStore.FindManyAsync(workflowDefIdSpec);

            var wfInstance = wfInstances.ToList().Find(wf => wf.Id == id);

            var interruptedWfInstances = await _workflowInterruptor.InterruptActivityTypeAsync(wfInstance, nameof(Sleep), cancellationToken: cancellationToken);

            return Ok($"Interrupted the workflow instance with id {interruptedWfInstances.FirstOrDefault()!.WorkflowInstance.Id} and type " + specId);
        }

        private WorkflowDefinitionIdSpecification GetWorkflowDefIdSpec(string workflowTypeName)
        {
            return new WorkflowDefinitionIdSpecification(workflowTypeName);
        }
    }
}

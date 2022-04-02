using Elsa.Persistence;
using Elsa.Persistence.Specifications.WorkflowInstances;
using Elsa.Providers.Workflow;
using Elsa.Providers.WorkflowContexts;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;

namespace P20550SuspendResume.Controllers
{
    //[Route("api/WorkflowDetails")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowDetailsController : Controller
    {
        private readonly IWorkflowBlueprintMaterializer _workflowBlueprintMaterializer;
        private readonly IWorkflowBlueprintReflector _workflowBlueprintReflector;
        private readonly IWorkflowContextManager _workflowContextManager;
        private readonly IWorkflowContextProvider _workflowContextProvider;
        private readonly IWorkflowDefinitionStore _workflowDefinitionStore;
        private readonly IWorkflowExecutionLog _workflowExecutionLog;
        private readonly IWorkflowExecutionLogStore _workflowExecutionLogStore;
        private readonly IWorkflowFactory _workflowFactory;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;
        private readonly IWorkflowProvider _workflowProvider;
        private readonly IWorkflowPublisher _workflowPublisher;
        private readonly IWorkflowRegistry _workflowRegistry;
        private readonly IWorkflowReviver _workflowReviver;
        private readonly IWorkflowRunner _workflowRunner;
        private readonly IWorkflowTriggerInterruptor _workflowTriggerInterruptor;


        public WorkflowDetailsController(

            IWorkflowBlueprintMaterializer workflowBlueprintMaterializer,
            IWorkflowBlueprintReflector workflowBlueprintReflector,
            IWorkflowContextManager workflowContextManager,
            //IWorkflowContextProvider workflowContextProvider,
            IWorkflowDefinitionStore workflowDefinitionStore,
            IWorkflowExecutionLog workflowExecutionLog,
            IWorkflowExecutionLogStore workflowExecutionLogStore,
            IWorkflowFactory workflowFactory,
            IWorkflowInstanceStore workflowInstanceStore,
            IWorkflowProvider workflowProvider,
            IWorkflowPublisher workflowPublisher,
            IWorkflowRegistry workflowRegistry,
            IWorkflowReviver workflowReviver,
            IWorkflowRunner workflowRunner,
            IWorkflowTriggerInterruptor workflowTriggerInterruptor
            )
        {
            _workflowBlueprintMaterializer = workflowBlueprintMaterializer;
            _workflowBlueprintReflector = workflowBlueprintReflector;
            _workflowContextManager = workflowContextManager;
            //_workflowContextProvider = workflowContextProvider;
            _workflowDefinitionStore = workflowDefinitionStore;
            _workflowExecutionLog = workflowExecutionLog;
            _workflowExecutionLogStore = workflowExecutionLogStore;
            _workflowFactory = workflowFactory;
            _workflowInstanceStore = workflowInstanceStore;
            _workflowProvider = workflowProvider;
            _workflowPublisher = workflowPublisher;
            _workflowRegistry = workflowRegistry;
            _workflowReviver = workflowReviver;
            _workflowRunner = workflowRunner;
            _workflowTriggerInterruptor = workflowTriggerInterruptor;

        }

        
        [HttpGet("GetWorkflowInstanceCount")]
        public async Task<ActionResult<int>> GetWorkflowInstanceCount()
        {
            var workflowsInRegistry = await _workflowRegistry.ListAsync();

            if (workflowsInRegistry.ToList().Count == 0)
                return 0; // If no workflows are registered, then no point going further.

            var workflowDefIdSpec = new WorkflowDefinitionIdSpecification(nameof(StartSuspendResumeWorkflow));

            var count = await _workflowInstanceStore.CountAsync(workflowDefIdSpec);

            var wfInstance = await _workflowInstanceStore.FindAsync(workflowDefIdSpec);

            var wfInstances = await _workflowInstanceStore.FindManyAsync(workflowDefIdSpec);           

            return count;
        }


        //[HttpGet("GetByName/{name}")]
        //public async Task<IActionResult> GetByName(string name)
        //{
        //    return Ok();
        //}

        [HttpGet("GetWorkflowDetails")]
        public async Task<ActionResult<WfInstanceDetails>> GetWorkflowDetails()
        {
            var workflowDefIdSpec = new WorkflowDefinitionIdSpecification(nameof(StartSuspendResumeWorkflow));

            var count = await _workflowInstanceStore.CountAsync(workflowDefIdSpec);

            //var wfInstance = await _workflowInstanceStore.FindAsync(workflowDefIdSpec);

            var wfInstances = await _workflowInstanceStore.FindManyAsync(workflowDefIdSpec);

            var wfInstanceDetails = new WfInstanceDetails();
            //{
            //    WfInstances = null,
            //    InstanceCount = 0
            //};

            if (count == 0)
                return wfInstanceDetails;           

            wfInstanceDetails.InstanceCount = count;
            wfInstanceDetails.WfInstances = new List<WfInstanceInfo>();

            foreach (var instance in wfInstances)
            {
                var wfInstanceInfo = new WfInstanceInfo
                {
                    Id = instance.Id,
                    Status = instance.WorkflowStatus.ToString(),
                };

                wfInstanceDetails.WfInstances.Add(wfInstanceInfo);
            }

            return wfInstanceDetails;
        }

    }

    public class WfInstanceDetails { 
        public List<WfInstanceInfo> WfInstances { get; set; }
        public int InstanceCount { get; set; }
    }

    public class WfInstanceInfo {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}

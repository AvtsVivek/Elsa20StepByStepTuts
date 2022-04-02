using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20570WakeupSleepingWorkflow.Controllers
{
    [ApiController]
    [Route("startworkflow")]
    public class StartWorkflowController : Controller
    {
        private readonly IBuildsAndStartsWorkflow _workflowRunner;

        public StartWorkflowController(IBuildsAndStartsWorkflow workflowRunner)
        {
            _workflowRunner = workflowRunner;
        }

        [HttpGet]
        public async Task<IActionResult> StartWorkflow()
        {
            var workflowInstance = await _workflowRunner.BuildAndStartWorkflowAsync<InterruptableWorkflow>();

            return new EmptyResult();
        }
    }
}

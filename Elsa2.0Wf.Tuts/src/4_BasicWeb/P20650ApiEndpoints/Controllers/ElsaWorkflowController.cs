using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20650ApiEndpoints.Controllers
{
    [ApiController]
    [Route("launch")]
    public class ElsaWorkflowController : Controller
    {
        private readonly IBuildsAndStartsWorkflow _workflowRunner;

        public ElsaWorkflowController(IBuildsAndStartsWorkflow workflowRunner)
        {
            _workflowRunner = workflowRunner;
        }

        [HttpGet]
        public async Task<IActionResult> Launch()
        {
            await _workflowRunner.BuildAndStartWorkflowAsync<HeartbeatWorkflow>();

            // Returning empty (the workflow will write an HTTP response).
            return new EmptyResult();
        }
    }
}

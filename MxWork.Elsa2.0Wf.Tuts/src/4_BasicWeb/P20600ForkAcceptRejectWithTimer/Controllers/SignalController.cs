using System.Threading;
using System.Threading.Tasks;
using Elsa.Activities.Signaling.Services;
using Microsoft.AspNetCore.Mvc;

namespace P20600ForkBranchWithTimerSingnalr.Controllers
{
    [ApiController]
    [Route("signal/{signalName}/trigger")]
    public class SignalController : Controller
    {
        private readonly ISignaler _signaler;

        public SignalController(ISignaler signaler)
        {
            _signaler = signaler;
        }

        [HttpGet]
        public async Task<JsonResult> Trigger(string signalName, string workflowInstanceId, CancellationToken cancellationToken)
        {
            await _signaler.TriggerSignalAsync(signalName, workflowInstanceId: workflowInstanceId, cancellationToken: cancellationToken);
            var jsonObj = Json("Signal triggered");
            // This json is not significant. This is becuase, the response is written by the worflow.
            return jsonObj;
        }
    }
}

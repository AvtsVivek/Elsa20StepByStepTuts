using Elsa.CustomActivityLibrary.Model;
using Elsa.CustomActivityLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elsa.Server.Web.Endpoints.Files
{
    [ApiController]
    [Route("files")]
    //[IgnoreAntiforgeryToken]
    public class Post : Controller
    {
        private readonly IFileReceivedInvoker _invoker;

        public Post(IFileReceivedInvoker invoker)
        {
            _invoker = invoker;
        }

        [HttpPost]
        public async Task<IActionResult> Handle(IFormFile file, CancellationToken cancellationToken)
        {
            var fileModel = new FileModel
            {
                FileName = Path.GetFileName(file.FileName),
                Content = await file.OpenReadStream().ReadBytesToEndAsync(cancellationToken),
                MimeType = file.ContentType
            };

            var collectedWorkflows = await _invoker.DispatchWorkflowsAsync(fileModel);
            return Ok(collectedWorkflows.ToList());
        }
    }
}

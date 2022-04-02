using Elsa.CustomActivityLibrary.Model;
using Elsa.Services.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elsa.CustomActivityLibrary.Services
{
    public interface IFileReceivedInvoker
    {
        // Is PendingWorkflow ok in place of CollectedWorkflow ?
        Task<IEnumerable<PendingWorkflow>> DispatchWorkflowsAsync(FileModel file=null, CancellationToken cancellationToken = default);
        // Is StartedWorkflow ok in place of CollectedWorkflow ?
        Task<IEnumerable<StartedWorkflow>> ExecuteWorkflowsAsync(FileModel file=null, CancellationToken cancellationToken = default);
    }
}

using Elsa.CustomActivityLibrary.Activities;
using Elsa.CustomActivityLibrary.BookMarks;
using Elsa.CustomActivityLibrary.Model;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elsa.CustomActivityLibrary.Services
{
    public class FileReceivedInvoker : IFileReceivedInvoker
    {
        private readonly IWorkflowLaunchpad _workflowLaunchpad;

        public FileReceivedInvoker(IWorkflowLaunchpad workflowLaunchpad)
        {
            _workflowLaunchpad = workflowLaunchpad;
        }

        public async Task<IEnumerable<PendingWorkflow>> DispatchWorkflowsAsync(FileModel file = null, CancellationToken cancellationToken = default)
        {
            var fileRecievedBookMark = new FileReceivedBookmark();
            // Need to Ask.
            // The following is ok? Same object for second and thrid parameter? or should be different for the two?
            var context = new CollectWorkflowsContext(nameof(FileReceived), fileRecievedBookMark, fileRecievedBookMark);
            return await _workflowLaunchpad.CollectAndDispatchWorkflowsAsync(context, file, cancellationToken);
        }

        public async Task<IEnumerable<StartedWorkflow>> ExecuteWorkflowsAsync(FileModel file = null, CancellationToken cancellationToken = default)
        {
            var fileRecievedBookMark = new FileReceivedBookmark();
            // Need to Ask.
            // The following is ok? Same object for second and thrid parameter? or should be different for the two?
            var context = new CollectWorkflowsContext(nameof(FileReceived), fileRecievedBookMark, fileRecievedBookMark);
            return await _workflowLaunchpad.CollectAndExecuteWorkflowsAsync(context, file, cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20734LoadWorkflowOnDashboard.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

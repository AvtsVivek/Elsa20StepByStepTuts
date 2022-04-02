using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = default!;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

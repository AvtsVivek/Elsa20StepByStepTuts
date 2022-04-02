using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elsa.Server.Web.Models
{
    public class WfInstanceDetails
    {
        public List<WfInstanceInfo> WfInstances { get; set; } = default!;
        public int InstanceCount { get; set; }
    }

    public class WfInstanceInfo
    {
        public string Id { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}

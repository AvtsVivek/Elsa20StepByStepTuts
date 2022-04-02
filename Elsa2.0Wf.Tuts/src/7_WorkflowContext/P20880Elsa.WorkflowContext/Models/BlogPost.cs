using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20880Elsa.WorkflowContext.Models
{
    public class BlogPost
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public bool IsPublished { get; set; }
    }
}

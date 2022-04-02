using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20590ForkBranch.Controllers
{
    public class WorkflowStartController : Controller
    {
        private readonly ILogger<WorkflowStartController> _logger;

        public WorkflowStartController(ILogger<WorkflowStartController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

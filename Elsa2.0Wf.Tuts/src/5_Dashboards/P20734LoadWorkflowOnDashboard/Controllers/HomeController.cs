using Elsa;
using Elsa.Builders;
using Elsa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using P20734LoadWorkflowOnDashboard.Models;
using System.Diagnostics;
using System.Linq;

namespace P20734LoadWorkflowOnDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ElsaOptions _elsaOptions;
        public HomeController(ILogger<HomeController> logger, 
            ElsaOptions elsaOptions)
        {
            var workflowType = typeof(HelloWorldBasicWorkflow);
            _elsaOptions = elsaOptions;

            var workflowFactory = _elsaOptions.WorkflowFactory;

            if (!workflowFactory.Types.Contains(workflowType))
                workflowFactory.Add(workflowType, provider => 
                (IWorkflow)ActivatorUtilities.GetServiceOrCreateInstance(provider, workflowType));
            
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ElsaWorkflow()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

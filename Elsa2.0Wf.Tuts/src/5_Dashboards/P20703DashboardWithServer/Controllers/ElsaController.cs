using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20703DashboardWithServer.Controllers
{
    public class ElsaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

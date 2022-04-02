using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace P20731ElsaDashboard.Pages
{
    public class ElsaDashboardModel : PageModel
    {
        public string ElsaServerUrl { get; set; }

        public ElsaDashboardModel(IConfiguration configuration)
        {
            var apiServerUrl = configuration.GetValue<string>("Elsa:Server:BaseUrl");
            ElsaServerUrl = apiServerUrl;
        }

        public void OnGet()
        {
        }
    }
}

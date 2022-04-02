using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P20731ElsaDashboard.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P20731ElsaDashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElsa();
            services.AddRazorPages();
            services.AddHttpClient<ElsaApiServerClient>(client =>
            {
                // What should be in place of backend?
                // backend elsa server api
                // This elsaBackendApi is from the tye.yaml file
                client.BaseAddress = Configuration.GetServiceUri("elsaBackendApi");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();            
            else
                app.UseExceptionHandler("/Error");

            app.UseAuthorization();

            app.UseStaticFiles() // For Dashboard.
            //.UseHttpActivities()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                    // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                    // For Dashboard.
                    //endpoints.MapFallbackToPage("/_Host");
                    //endpoints.MapFallbackToPage("/ElsaWorkflows/{**segment}", "/_Host");

                    endpoints.MapRazorPages();
            });
        }
    }
}

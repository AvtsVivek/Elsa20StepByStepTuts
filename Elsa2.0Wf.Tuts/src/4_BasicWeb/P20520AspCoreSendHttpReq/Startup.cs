using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20520AspCoreSendHttpReq
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddElsa(options => options
                .AddHttpActivities()
                .AddWorkflow<HelloHttpWorkflow>()
                .AddWorkflow<SendHttpWorkflow>()
                ).AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpActivities();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();            
            else            
                app.UseExceptionHandler("/Error");
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //app.UseWelcomePage();
        }
    }
}

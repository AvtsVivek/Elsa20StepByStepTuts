using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Elsa;
using P20593ForkBranchWithTimerSignaler.Workflows;

namespace P20593ForkBranchWithTimerSignaler
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
            services.AddControllersWithViews();

            services
                .AddElsa(options => options
                    .AddConsoleActivities()
                    //.AddHttpActivities()
                    .AddHttpActivities(httpOptions => Configuration.GetSection("Elsa:Http").Bind(httpOptions))
                    .AddQuartzTemporalActivities()
                    //.AddActivity<Sleep>()
                    //.AddActivity<ReadQueryString>()
                    //.AddWorkflow<EchoQueryStringWorkflow>()
                    //.AddWorkflow<DemoWorkflow>()
                    .AddWorkflow<TimerSignalerWorkflow>()
                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // Add HTTP activities middleware.
            app.UseHttpActivities();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

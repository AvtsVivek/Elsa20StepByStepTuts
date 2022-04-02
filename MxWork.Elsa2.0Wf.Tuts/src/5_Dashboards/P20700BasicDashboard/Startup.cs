using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Elsa;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20700BasicDashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var elsaSection = Configuration.GetSection("Elsa");
            services.AddControllersWithViews();
            services.AddRazorPages();
            services
                .AddElsa(options => options
                .AddConsoleActivities()
                .AddHttpActivities()
                .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                //.AddHttpActivities(httpOptions => Configuration.GetSection("Elsa:Http").Bind(httpOptions))
                .AddQuartzTemporalActivities()
                .AddWorkflow<HeartbeatWorkflow>()
        );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            

            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpActivities();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapFallbackToPage("/_Host");
                //endpoints.MapFallbackToPage("/Hare/{**segment}", "/_Host");

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapRazorPages();
            });

            //app.UseWhen(context => context.Request.Path == "/workflows",
            //    appBuilder => appBuilder.Map("/_Host"));
        }
    }
}

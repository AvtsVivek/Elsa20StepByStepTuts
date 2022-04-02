using Elsa;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20650ApiEndpoints
{
    public class Startup
    {
        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services
                .AddElsa(elsa => elsa
                    //.UseEntityFrameworkPersistence(ef => ef.UseSqlite())
                    .AddConsoleActivities()
                    .AddHttpActivities()
                    //.AddHttpActivities(httpOptions => Configuration.GetSection("Elsa:Http").Bind(httpOptions))
                    .AddQuartzTemporalActivities()
                    //.AddJavaScriptActivities()
                    .AddWorkflow<HeartbeatWorkflow>()
                );

            // Dont know what this does. Is this required for this example?
            //services.AddApiVersioning();
            // Elsa API endpoints.
            services.AddElsaApiEndpoints();

            // I think this cors stuff is needed for advanced or productioin stuff. 
            // This is not needed for this example.
            // Allow arbitrary client browser apps to access the API.
            // In a production environment, make sure to allow only origins you trust.
            //services.AddCors(cors => cors.AddDefaultPolicy(policy => policy
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowAnyOrigin()
            //    .WithExposedHeaders("Content-Disposition"))
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            
            app.UseRouting();

            // Add HTTP activities middleware.
            app.UseHttpActivities();

            // Cors is not needed for this example. May be needed for advanced stuff.
            //app.UseCors();


            app
                .UseEndpoints(endpoints =>
                {
                    // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                    //endpoints.MapControllers();
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });

        }
    }
}

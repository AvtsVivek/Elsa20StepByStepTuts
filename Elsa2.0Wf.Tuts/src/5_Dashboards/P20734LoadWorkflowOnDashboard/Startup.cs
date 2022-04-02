using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Activities;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20734LoadWorkflowOnDashboard
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
            var elsaSection = Configuration.GetSection("Elsa");

            // Elsa services.
            services
                .AddElsa(elsaOptions => elsaOptions
                    //.UseEntityFrameworkPersistence(ef => ef.UseSqlite())
                    //.UseEntityFrameworkPersistence(ef =>
                    //{
                    //    //ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa20;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    //})
                    .AddConsoleActivities()
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddQuartzTemporalActivities()
                    .AddActivitiesFrom<HelloWorld>()
                    //.AddWorkflow<HelloWorldWorkflow>()
                );

            // Elsa API endpoints.
            services.AddElsaApiEndpoints();

            // Swagger
            services.AddElsaSwagger();

            // For Dashboard.
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();

                // The following is just trials. The defualt is swagger
                var swaggerRoutTemplateBasePath = "api-customroute";

                app.UseSwagger(options =>
                {
                    options.RouteTemplate = swaggerRoutTemplateBasePath + "/{documentName}/swagger.json";

                    options.PreSerializeFilters.Add((swagger, httpReq) =>
                    { 
                    });
                });

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/" + swaggerRoutTemplateBasePath + "/v1/swagger.json", "Elsa"));
            }

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});



            app
                .UseStaticFiles() // For Dashboard.
                .UseHttpActivities()
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

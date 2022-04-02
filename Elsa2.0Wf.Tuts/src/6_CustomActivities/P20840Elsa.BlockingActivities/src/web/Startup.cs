using Elsa.CustomActivityLibrary.Activities;
using Elsa.CustomActivityLibrary.BookMarks;
using Elsa.CustomActivityLibrary.Liquid;
using Elsa.CustomActivityLibrary.Services;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa.Server.Web.Extensions;
using Elsa.Server.Web.HostedService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Elsa.Server.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var elsaSection = Configuration.GetSection("Elsa");
            // Elsa services.
            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef =>
                    {
                        ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa20;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    })
                    .AddConsoleActivities()
                    .AddEmailActivities(options => Configuration.GetSection("Elsa:Smtp").Bind(options))
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddQuartzTemporalActivities()
                    .AddWorkflowsFrom<Startup>()
                    .AddActivity<FileReceived>()
                );

            services.AddBookmarkProvider<FileReceivedBookmarkProvider>();
            services.AddNotificationHandlersFrom<LiquidHandler>();

            services.AddScoped<IFileReceivedInvoker, FileReceivedInvoker>();
            services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
            services.AddHostedService<FileMonitorService>();
            // Elsa API endpoints.
            services.AddElsaApiEndpoints();

            services.AddElsaSwagger();

            // For Dashboard.
            services.AddRazorPages();

            // Allow arbitrary client browser apps to access the API.
            // In a production environment, make sure to allow only origins you trust.
            services.AddCors(cors => cors.AddDefaultPolicy(policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders("Content-Disposition"))
            );
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

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


            app
                .UseStaticFiles() // For Dashboard.
                .UseHttpActivities()
                .UseCors()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                    //endpoints.MapControllers();
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapRazorPages();
                })
                //.UseWelcomePage()
                ;
        }
    }
}

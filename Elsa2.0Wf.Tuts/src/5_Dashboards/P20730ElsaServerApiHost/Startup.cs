using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//P20730ElsaServerApiHost
namespace P20730ElsaServerApiHost
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
            var elsaSection = Configuration.GetSection("Elsa");
            string sqlConnectionString = this.Configuration.GetConnectionString("Default");

            // Elsa services.
            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef =>
                    {
                        ef.UseSqlServer(sqlConnectionString);
                    })

                //.AddConsoleActivities()
                //.AddQuartzTemporalActivities()
                .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                //.AddWorkflowsFrom<Startup>()
                );

            // Elsa API endpoints.
            services.AddElsaApiEndpoints();

            // Swagger
            services.AddElsaSwagger();
            //services.AddRazorPages();


            // Allow arbitrary client browser apps to access the API for demo purposes only.
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
            {
                app.UseDeveloperExceptionPage();

                var swaggerRoutTemplateBasePath = "api-customroute";

                app.UseSwagger(options =>
                {
                    options.RouteTemplate = swaggerRoutTemplateBasePath + "/{documentName}/swagger.json";

                    options.PreSerializeFilters.Add((swagger, httpReq) => {});
                });

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/" + swaggerRoutTemplateBasePath + "/v1/swagger.json", "Elsa"));

            }
            else
                app.UseExceptionHandler("/Error");
            

            app
            .UseRouting()
            //.UseStaticFiles() // For Dashboard.
            //.UseHttpActivities()

            .UseEndpoints(endpoints =>
            {
                // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                endpoints.MapControllers();
            })
            ;

        }
    }
}

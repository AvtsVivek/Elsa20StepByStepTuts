using Elsa;
using Elsa.Activities.UserTask.Extensions;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa.Runtime;
using P20880Elsa.WorkflowContext.Data;
using P20880Elsa.WorkflowContext.Data.StartupTasks;
using P20880Elsa.WorkflowContext.Providers.WorkflowContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// P20880Elsa.WorkflowContext
namespace P20880Elsa.WorkflowContext 
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var elsaSection = Configuration.GetSection("Elsa");
            var sqlConnectionString = Configuration.GetConnectionString("MsSqlServer");

            services
                .AddDbContextFactory<BlogContext>(options => options.UseSqlServer(
                    sqlConnectionString, sql => sql.MigrationsAssembly(typeof(Startup).Assembly.FullName)
                    ))
                .AddCors(cors => cors.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()))
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef =>
                    {
                        ef.UseSqlServer(sqlConnectionString);
                    })
                    .AddConsoleActivities()
                    .AddHttpActivities(options => options.BasePath = "/workflows")
                    //.AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddJavaScriptActivities()
                    .AddQuartzTemporalActivities()
                    .AddUserTaskActivities()
                    .AddEmailActivities(options =>
                        Configuration.GetSection("Elsa:Smtp").Bind(options)
                        )
                    )

                .AddWorkflowContextProvider<BlogPostWorkflowContextProvider>()
                .AddStartupTask<RunBlogMigrations>()
                ;
            services.AddElsaApiEndpoints();
            services.AddElsaSwagger();
            services.AddRazorPages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elsa"));
            }
            else
                app.UseExceptionHandler("/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpActivities();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapRazorPages();
            });
        }
    }
}

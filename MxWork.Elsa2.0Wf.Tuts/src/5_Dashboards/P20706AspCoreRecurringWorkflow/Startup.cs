using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace P20706AspCoreRecurringWorkflow
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
        public void ConfigureServices(IServiceCollection services)
        {
            //var elsaSection = Configuration.GetSection("Elsa");

            services.AddRazorPages();

            services
                .AddElsa(options => options
                .UseEntityFrameworkPersistence(ef =>
                {
                    ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa20;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                })
                .AddConsoleActivities()
                //.AddHttpActivities(elsaSection.GetSection("Server").Bind)
                .AddHttpActivities()
                .AddQuartzTemporalActivities()
                );

            services
                .AddElsaApiEndpoints();
            // For Dashboard.
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseHttpActivities();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

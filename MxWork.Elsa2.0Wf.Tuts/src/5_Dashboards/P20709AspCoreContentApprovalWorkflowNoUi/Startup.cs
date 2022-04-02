using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;

namespace P20709AspCoreContentApprovalWorkflowNoUi
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

            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef =>
                    {
                        ef.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa202;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    })
                    .AddConsoleActivities()
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                    .AddQuartzTemporalActivities()
                    .AddWorkflow<DocumentApprovalWorkflow>()
                );

            services.AddElsaApiEndpoints();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseStaticFiles()
                .UseHttpActivities()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToPage("/_Host");
                });
        }
    }
}

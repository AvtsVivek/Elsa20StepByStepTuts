using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace P20880Elsa.WorkflowContext.Data
{
    public class MsSqlBlogContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BlogContext>();
            var connectionString = args.Any() ? args[0] : @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Elsa21;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            builder.UseSqlServer(connectionString, db => db
                .MigrationsAssembly(typeof(MsSqlBlogContextFactory).Assembly.GetName().Name));

            return new BlogContext(builder.Options);
        }
    }
}

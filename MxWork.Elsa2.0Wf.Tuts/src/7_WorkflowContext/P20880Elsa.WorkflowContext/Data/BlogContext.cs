using P20880Elsa.WorkflowContext.Models;
using Microsoft.EntityFrameworkCore;

namespace P20880Elsa.WorkflowContext.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
    }
}

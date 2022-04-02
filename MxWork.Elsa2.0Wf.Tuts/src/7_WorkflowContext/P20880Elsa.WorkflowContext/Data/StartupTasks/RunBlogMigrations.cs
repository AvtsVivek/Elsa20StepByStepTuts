using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Services;
using Microsoft.EntityFrameworkCore;

namespace P20880Elsa.WorkflowContext.Data.StartupTasks
{
    public class RunBlogMigrations : IStartupTask
    {
        private readonly IDbContextFactory<BlogContext> _dbContextFactory;

        public RunBlogMigrations(IDbContextFactory<BlogContext> dbContextFactoryFactory)
        {
            _dbContextFactory = dbContextFactoryFactory;
        }

        public int Order => 0;

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            await dbContext.Database.MigrateAsync(cancellationToken);
            await dbContext.DisposeAsync();
        }
    }
}

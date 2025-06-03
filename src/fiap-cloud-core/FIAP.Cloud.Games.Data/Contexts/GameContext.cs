using FIAP.Cloud.Games.Application.Games.Abstractions;
using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FIAP.Cloud.Games.Data.Contexts
{
    public class GameContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        public DbSet<Game> Games { get; set; }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null || entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    entry.Property("UpdatedAt").IsModified = false;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken) > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ErrorDetail>();

            base.OnModelCreating(modelBuilder);
        }

        public DbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }
    }
}

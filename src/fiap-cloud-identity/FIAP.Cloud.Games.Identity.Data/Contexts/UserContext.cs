using FIAP.Cloud.Games.Identity.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Cloud.Games.Identity.Data.Contexts
{
    public class UserContext(DbContextOptions<UserContext> options) : IdentityDbContext<CurrentUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}

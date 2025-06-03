using FIAP.Cloud.Games.Identity.Data.Rules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Cloud.Games.Identity.Data.Configurations.Mappings
{
    public class IdentityRoleMapping : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            List<IdentityRole> roles = [
                new() { Name = IdentityRolesConst.Admin.Key, NormalizedName = IdentityRolesConst.Admin.Value },
                new() { Name = IdentityRolesConst.User.Key, NormalizedName = IdentityRolesConst.User.Value }
            ];

            builder.HasData(roles);
        }
    }
}

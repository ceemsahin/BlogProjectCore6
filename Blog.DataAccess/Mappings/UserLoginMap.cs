using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class UserLoginMap : IEntityTypeConfiguration<AppUserLogin>
    {
        public void Configure(EntityTypeBuilder<AppUserLogin> builder)
        {
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            builder.Property(l => l.LoginProvider);
            builder.Property(l => l.ProviderKey);


            builder.ToTable("AspNetUserLogins");

        }
    }
}

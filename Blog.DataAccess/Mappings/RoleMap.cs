using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName).HasName("RoleNameIndex").IsUnique();
            builder.ToTable("AspNetRoles");

            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            builder.HasData(new AppRole
            {
                Id = Guid.Parse("F6C56659-17C6-4AF1-8729-69F86DDCBF1D"),
                Name = "Superadmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new AppRole
            {
                Id = Guid.Parse("65E0C8FE-0963-4BC9-B536-3118D1662152"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
              new AppRole
              {
                  Id = Guid.Parse("633F32E5-93C0-40BB-9196-66D1F2A46C42"),
                  Name = "User",
                  NormalizedName = "USER",
                  ConcurrencyStamp = Guid.NewGuid().ToString()
              }
            );
        }
    }
}

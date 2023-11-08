using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {

            builder.HasKey(r => new { r.UserId, r.RoleId });
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("5B406862-9DAD-468C-A5A7-3232B6B8548E"),
                RoleId = Guid.Parse("F6C56659-17C6-4AF1-8729-69F86DDCBF1D")
            },
            new AppUserRole
            {
                UserId = Guid.Parse("6B7FD6DF-3542-44E9-9E15-9A0B279D1701"),
                RoleId = Guid.Parse("65E0C8FE-0963-4BC9-B536-3118D1662152")
            });
        }
    }
}

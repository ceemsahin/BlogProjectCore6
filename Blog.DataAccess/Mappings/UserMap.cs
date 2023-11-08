using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class UserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {


            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(x => x.NormalizedEmail).HasName("EmailIndex");

            builder.ToTable("AspNetUsers");

            builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(x => x.UserName).HasMaxLength(256);
            builder.Property(x => x.NormalizedUserName).HasMaxLength(256);
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.NormalizedEmail).HasMaxLength(256);

            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(xc => xc.UserId).IsRequired();
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(xc => xc.UserId).IsRequired();
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(xc => xc.UserId).IsRequired();
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(xc => xc.UserId).IsRequired();


            var superAdmin = new AppUser
            {
                Id = Guid.Parse("5B406862-9DAD-468C-A5A7-3232B6B8548E"),
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                Email = "superadmin@gmail.com",
                PhoneNumber = "+905439999999",
                FirstName = "Cem",
                LastName = "Şahin",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("516EAF4E-3701-4AC4-ACBF-410E4DA5261F")

            };
            superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "123456");

            var admin = new AppUser
            {
                Id = Guid.Parse("6B7FD6DF-3542-44E9-9E15-9A0B279D1701"),
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                PhoneNumber = "+905439999998",
                FirstName = "Cemo",
                LastName = "Şahino",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("BF66EB62-30E8-4F1A-A8FE-7D65A84581EB")

            };
            admin.PasswordHash = CreatePasswordHash(admin, "123456");

            builder.HasData(superAdmin, admin);
        }
        private string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}

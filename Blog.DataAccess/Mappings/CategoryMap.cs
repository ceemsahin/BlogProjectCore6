using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = Guid.Parse("81CA1870-8FF0-4C5B-8BBD-B6210380D48A"),
                Name = "Core mvc",
                CreatedBy = "Cem",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
             new Category
             {
                 Id = Guid.Parse("0057B95D-5C0F-415E-AD50-201C6DF5F7CB"),
                 Name = "Core mvc 2",
                 CreatedBy = "Cem2",
                 CreatedDate = DateTime.Now,
                 IsDeleted = false
             });
        }
    }
}

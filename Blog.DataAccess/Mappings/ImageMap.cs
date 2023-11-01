using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                Id = Guid.Parse("516EAF4E-3701-4AC4-ACBF-410E4DA5261F"),
                FileName = " images/testimage",
                FileType = "jpg",
                CreatedBy = "Cem",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
            new Image
            {
                Id = Guid.Parse("BF66EB62-30E8-4F1A-A8FE-7D65A84581EB"),
                FileName = " images/testimage2",
                FileType = "png",
                CreatedBy = "Cem2",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            });
        }
    }
}

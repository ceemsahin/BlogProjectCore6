using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article
            {
                Id = Guid.NewGuid(),
                Title = "Test1",
                Content = "Lorem ipsum",
                ViewCount = 15,               
                CategoryId = Guid.Parse("81CA1870-8FF0-4C5B-8BBD-B6210380D48A"),                    
                ImageId  = Guid.Parse("516EAF4E-3701-4AC4-ACBF-410E4DA5261F"),
                CreatedBy = "Cem",
                CreatedDate = DateTime.Now,
                IsDeleted = false

            },
            new Article
            {
                Id = Guid.NewGuid(),
                Title = "Test2",
                Content = "Lorem ipsum2",
                ViewCount = 1,
                CategoryId = Guid.Parse("0057B95D-5C0F-415E-AD50-201C6DF5F7CB"),
                ImageId = Guid.Parse("BF66EB62-30E8-4F1A-A8FE-7D65A84581EB"),
                CreatedBy = "Cem2",
                CreatedDate = DateTime.Now,
                IsDeleted = false

            });
        }
    }
}

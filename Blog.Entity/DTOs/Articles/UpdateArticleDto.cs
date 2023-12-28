using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.Articles
{
    public class UpdateArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public Image Image { get; set; }
        public IFormFile? Photo { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}

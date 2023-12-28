using Blog.Entity.DTOs.Categories;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.Articles
{
    public class AddArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile Photo { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}

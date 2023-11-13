using Blog.Entity.DTOs.Categories;

namespace Blog.Entity.DTOs.Articles
{
    public class AddArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}

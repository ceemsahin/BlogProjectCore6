using Blog.Entity.DTOs.Articles;

namespace Blog.Business.Services.Abstract
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync();

        Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
        Task CreateArticleAsync(AddArticleDto articleAdd);
        Task UpdateArticleAsync(UpdateArticleDto updateArticleDto);
    }
}

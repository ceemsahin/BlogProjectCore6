using Blog.Entity.DTOs.Articles;

namespace Blog.Business.Services.Abstract
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync();
        Task<List<ArticleDto>> GetAllDeletedArticlesWithCategoryAsync();
        Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
        Task CreateArticleAsync(AddArticleDto articleAdd);
        Task<string> UpdateArticleAsync(UpdateArticleDto updateArticleDto);
        Task<string> SafeDeleteArticleAsync(Guid articleId);
        Task<string> UndoDeleteArticleAsync(Guid articleId);
    }
}

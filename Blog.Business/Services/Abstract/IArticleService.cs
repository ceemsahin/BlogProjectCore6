using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Business.Services.Abstract
{
    public interface IArticleService
    {
        Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync();

        Task CreateArticleAsync(AddArticleDto articleAdd);
    }
}

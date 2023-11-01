using Blog.Entity.Entities;

namespace Blog.Business.Services.Abstract
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticleAsync();

    }
}

using AutoMapper;
using Blog.Business.Services.Abstract;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Business.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateArticleAsync(AddArticleDto articleAdd)
        {
            var userId = Guid.Parse("5B406862-9DAD-468C-A5A7-3232B6B8548E");

            var article = new Article
            {
                UserId = userId,
                Title = articleAdd.Title,
                Content = articleAdd.Content,
                CategoryId = articleAdd.CategoryId
            };

            await _unitOfWork.GetRepository<Article>().AddAsync(article);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleDto>> GetAllArticleWithCategoryNonDeletedAsync()
        {
            var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted, x => x.Category);
            var map = _mapper.Map<List<ArticleDto>>(articles);
            return map;

        }
        public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
        {
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category);
            var map = _mapper.Map<ArticleDto>(article);
            return map;

        }
        public async Task UpdateArticleAsync(UpdateArticleDto updateArticleDto)
        {
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x=>!x.IsDeleted && x.Id == updateArticleDto.Id,x=>x.Category);

            article.Title = updateArticleDto.Title;
            article.Content = updateArticleDto.Content;
            article.CategoryId = updateArticleDto.CategoryId; 

            await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();
        }
    }
}

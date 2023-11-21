using AutoMapper;
using Blog.Business.Extensions;
using Blog.Business.Services.Abstract;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blog.Business.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal _user;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, ClaimsPrincipal principal, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = _contextAccessor.HttpContext.User;
            _contextAccessor = contextAccessor;
        }

        public async Task CreateArticleAsync(AddArticleDto articleAddDto)
        {
            var userId = _user.GetLoggedInUserId(); 
            var userEmail = _user.GetLoggedInEmail(); 
            var imageId = Guid.Parse("516EAF4E-3701-4AC4-ACBF-410E4DA5261F");
            var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, articleAddDto.CategoryId, imageId,userEmail);
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
        public async Task<string> UpdateArticleAsync(UpdateArticleDto updateArticleDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == updateArticleDto.Id, x => x.Category);

            article.Title = updateArticleDto.Title;
            article.Content = updateArticleDto.Content;
            article.CategoryId = updateArticleDto.CategoryId;
            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = userEmail;

            await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return article.Title;
        }

        public async Task<string> SafeDeleteArticleAsync(Guid articleId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);

            article.IsDeleted = true;
            article.DeletedDate = DateTime.Now;
            article.DeletedBy = userEmail;

            await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return article.Title;
        }


    }
}

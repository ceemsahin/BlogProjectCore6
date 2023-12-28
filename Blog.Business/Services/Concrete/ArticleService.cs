using AutoMapper;
using Blog.Business.Extensions;
using Blog.Business.Helpers.Images;
using Blog.Business.Services.Abstract;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blog.Business.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IImageHelper _imageHelper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IImageHelper imageHelper, ClaimsPrincipal user = null)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = httpContextAccessor.HttpContext.User;
            _httpContextAccessor = httpContextAccessor;
            _imageHelper = imageHelper;
        }

        public async Task CreateArticleAsync(AddArticleDto articleAddDto)
        {
            var userId = _user.GetLoggedInUserId();
            var userEmail = _user.GetLoggedInEmail();
            var imageUpload = await _imageHelper.Upload(articleAddDto.Title, articleAddDto.Photo, ImageTypes.Post);
            Image image = new(imageUpload.FullName, articleAddDto.Photo.ContentType, userEmail);
            await _unitOfWork.GetRepository<Image>().AddAsync(image);
            var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, articleAddDto.CategoryId, image.Id, userEmail);
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
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleId, x => x.Category, i => i.Image);
            var map = _mapper.Map<ArticleDto>(article);
            return map;

        }
        public async Task<string> UpdateArticleAsync(UpdateArticleDto updateArticleDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var article = await _unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == updateArticleDto.Id, x => x.Category, i => i.Image);

            if (updateArticleDto.Photo != null)
            {
                _imageHelper.Delete(article.Image.FileName);

                var imageUpload = await _imageHelper.Upload(updateArticleDto.Title, updateArticleDto.Photo, ImageTypes.Post);
                Image image = new Image(imageUpload.FullName, updateArticleDto.Photo.ContentType, userEmail);

                await _unitOfWork.GetRepository<Image>().AddAsync(image);
                article.ImageId = image.Id;
            }

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

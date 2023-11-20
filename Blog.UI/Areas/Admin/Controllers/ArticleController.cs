using AutoMapper;
using Blog.Business.Extensions;
using Blog.Business.Services.Abstract;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;
using Blog.UI.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<Article> _validator;
        private readonly IToastNotification _toastNotification;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IValidator<Article> validator, IToastNotification toastNotification)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
            _validator = validator;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticleWithCategoryNonDeletedAsync();
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(new AddArticleDto { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArticleDto addArticleDto)
        {
            var map = _mapper.Map<Article>(addArticleDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _articleService.CreateArticleAsync(addArticleDto);
                _toastNotification.AddSuccessToastMessage(Messages.Article.Add(addArticleDto.Title), new ToastrOptions { Title = "Başarılı!" });
                RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else
            {
                result.AddToModelState(this.ModelState);
            }

            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(new AddArticleDto { Categories = categories });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid articleId)
        {

            var article = await _articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
            var categories = await _categoryService.GetAllCategoriesNonDeleted();

            var articleUpdateDto = _mapper.Map<UpdateArticleDto>(article);
            articleUpdateDto.Categories = categories;

            return View(articleUpdateDto);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateArticleDto updateArticleDto)
        {
            var map = _mapper.Map<Article>(updateArticleDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var title = await _articleService.UpdateArticleAsync(updateArticleDto);
                _toastNotification.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else
            {
                result.AddToModelState(this.ModelState);
            }

            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            updateArticleDto.Categories = categories;

            return View(updateArticleDto);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid articleId)
        {
            var title = await _articleService.SafeDeleteArticleAsync(articleId);
            _toastNotification.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
    }
}
using AutoMapper;
using Blog.Business.Services.Abstract;
using Blog.Entity.DTOs.Articles;
using Microsoft.AspNetCore.Mvc;

namespace Blog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
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

            await _articleService.CreateArticleAsync(addArticleDto);
            RedirectToAction("Index", "Article", new { Area = "Admin" });
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
            await _articleService.UpdateArticleAsync(updateArticleDto);
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            updateArticleDto.Categories = categories;

            return View(updateArticleDto);

        }


    }
}

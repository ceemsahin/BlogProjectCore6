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

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
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
            RedirectToAction("Index","Article",new { Area = "Admin"});
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(new AddArticleDto { Categories = categories });
        }

    }
}

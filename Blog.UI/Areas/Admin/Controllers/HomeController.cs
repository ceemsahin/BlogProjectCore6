using Blog.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Blog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var articles =  await _articleService.GetAllArticleAsync();
            return View(articles);
        }
    }
}

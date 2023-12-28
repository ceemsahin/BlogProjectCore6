using AutoMapper;
using Blog.Business.Services.Abstract;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Blog.UI.ResultMessages;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<Category> _validator;
        private readonly IToastNotification _toastNotification;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IToastNotification toastNotification, IValidator<Category> validator)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _validator = validator;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryDto addCategoryDto)
        {
            var map = _mapper.Map<Category>(addCategoryDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await _categoryService.CreateCategoryAsync(addCategoryDto);
                _toastNotification.AddSuccessToastMessage(Messages.Category.Add(addCategoryDto.Name), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            result.AddToModelState(this.ModelState);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            var map = _mapper.Map<Category, UpdateCategoryDto>(category);

            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto updateCategoryDto)
        {
            var map = _mapper.Map<Category>(updateCategoryDto);
            var result = await _validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var name = await _categoryService.UpdateCategoryAsync(updateCategoryDto);
                _toastNotification.AddSuccessToastMessage(Messages.Category.Update(name), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

            result.AddToModelState(this.ModelState);
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await _categoryService.SafeDeleteCategoryAsync(categoryId);
            _toastNotification.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }
    }
}

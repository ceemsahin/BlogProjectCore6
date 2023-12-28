using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Business.Services.Abstract
{
    public interface ICategoryService
    {

        Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
        Task CreateCategoryAsync(AddCategoryDto addCategoryDto);
        Task<Category> GetCategoryById(Guid categoryId);
        Task<string> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);

    }
}

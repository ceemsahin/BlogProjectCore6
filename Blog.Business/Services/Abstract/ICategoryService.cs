using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Business.Services.Abstract
{
    public interface ICategoryService
    {

        Task<List<CategoryDto>> GetAllCategoriesNonDeletedAsync();
        Task<List<CategoryDto>> GetAllCategoriesDeletedAsync();
        Task CreateCategoryAsync(AddCategoryDto addCategoryDto);
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<string> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);
        Task<string> UndoDeleteCategoryAsync(Guid categoryId);

    }
}

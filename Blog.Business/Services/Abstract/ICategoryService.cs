using Blog.Entity.DTOs.Categories;

namespace Blog.Business.Services.Abstract
{
    public interface ICategoryService
    {

        public Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
    }
}

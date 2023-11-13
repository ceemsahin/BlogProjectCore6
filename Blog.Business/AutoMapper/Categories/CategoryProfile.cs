using AutoMapper;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Business.AutoMapper.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}

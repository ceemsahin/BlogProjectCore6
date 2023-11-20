using AutoMapper;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Business.AutoMapper.Articles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDto, Article>().ReverseMap();
            CreateMap<UpdateArticleDto, Article>().ReverseMap();
            CreateMap<UpdateArticleDto, ArticleDto>().ReverseMap();
            CreateMap<AddArticleDto, Article>().ReverseMap();
        }

    }
}

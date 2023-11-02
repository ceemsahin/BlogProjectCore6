using AutoMapper;
using Blog.Business.Services.Abstract;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.Articles;
using Blog.Entity.Entities;

namespace Blog.Business.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ArticleDto>> GetAllArticleAsync()
        {
            var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync();
            var map = _mapper.Map<List<ArticleDto>>(articles);
            return map;

        }
    }
}

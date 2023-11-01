using Blog.Business.Services.Abstract;
using Blog.Business.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business.Extensions
{
    public static class BusinessExtensions
    {

        public static IServiceCollection LoadBusinessExtensions(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            return services;
        }
    }
}

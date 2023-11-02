using Blog.Business.Services.Abstract;
using Blog.Business.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Business.Extensions
{
    public static class BusinessExtensions
    {
        public static IServiceCollection LoadBusinessExtensions(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IArticleService, ArticleService>();
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}

using Blog.Business.FluentValidations;
using Blog.Business.Helpers.Images;
using Blog.Business.Services.Abstract;
using Blog.Business.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IImageHelper, ImageHelper>();    
            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("tr-TR");
            });

            return services;
        }
    }
}

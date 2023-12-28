using Blog.Entity.DTOs.Images;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Http;

namespace Blog.Business.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageTypes imageType, string folderName = null);
        void Delete(string imageName);
    }
}

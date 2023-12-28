using Blog.Entity.DTOs.Images;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Blog.Business.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly string wwwroot;
        private readonly IWebHostEnvironment _env;

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            wwwroot = _env.WebRootPath;
        }

        private string ReplaceInvalidChars(string fileName)
        {
            return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
        }

        private const string _imageFolder = "images";
        private const string _articleImageFolder = "article-images";
        private const string _userImageFolder = "user-images";

        public void Delete(string imageName)
        {
            var fileToDelete = Path.Combine($"{wwwroot}/{_imageFolder}/{imageName}");
            if (File.Exists(fileToDelete))
                File.Delete(fileToDelete);
        }

        public async Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageTypes imageType, string folderName = null)
        {
            folderName ??= imageType == ImageTypes.User ? _userImageFolder : _articleImageFolder;

            if (!Directory.Exists($"{wwwroot}/{_imageFolder}/{folderName}"))
                Directory.CreateDirectory($"{wwwroot}/{_imageFolder}/{folderName}");

            string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtensions = Path.GetExtension(imageFile.FileName);

            name = ReplaceInvalidChars(name);

            DateTime dateTime = DateTime.Now;
            string newFileName = $"{name}_{dateTime.Millisecond}{fileExtensions}";
            var path = Path.Combine($"{wwwroot}/{_imageFolder}/{folderName}", newFileName);

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            string message = imageType == ImageTypes.User
                ? $"{newFileName} isimli kullanıcı resmi başarıyla eklenmiştir."
                : $"{newFileName} isimli makale resmi başarıyla eklenmiştir.";


            return new ImageUploadedDto()
            {
                FullName = $"{folderName}/{newFileName}"
            };
        }
    }
}

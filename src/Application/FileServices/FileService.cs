using Application.Common.Halpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.FileServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string MEDIA = "media";
        private readonly string IMAGES = "images";
        private readonly string ROOTPATH;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async ValueTask<byte[]> GetVideoAsync(string filepath)
        {
            string path = Path.Combine(filepath);
            byte[] imageBytes = await File.ReadAllBytesAsync(path);
            return imageBytes;
        }

        public async ValueTask<string> UploadVideoAsync(IFormFile imagepath)
        {
            string newImageName = MediaHelper.MakeVideoImageName(imagepath.FileName);
            string subPath = Path.Combine(MEDIA, IMAGES, newImageName);

          

            FileStream fileStream = new FileStream(subPath, FileMode.Create);
            await imagepath.CopyToAsync(fileStream);
            fileStream.Close();

            return subPath;
        }
    }
}

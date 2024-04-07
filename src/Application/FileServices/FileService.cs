using Domain.Exceptions.Videos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.FileServices
{
    public class FileService : IFileService
    {
        private readonly string ROOTPATH;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            ROOTPATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public async ValueTask<byte[]> GetImageAsync(string fileName)
        {
            string path = Path.Combine(ROOTPATH, fileName);
            byte[] imageBytes = await File.ReadAllBytesAsync(path);
            return imageBytes;
        }

        public async ValueTask<string> UploadImageAsync(IFormFile file)
        {
            string filePath = "";
            string fileName = "";

            try
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "media","images" ,fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return "/media/images/" + fileName;
            }
            catch (VideoNotValid)
            {
                throw new VideoNotValid();
            }
        }
    }
}

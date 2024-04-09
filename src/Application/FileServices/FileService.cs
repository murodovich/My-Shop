using Application.Common.Halpers;
using Domain.Exceptions.Videos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.FileServices
{
    public class FileService : IFileService
    {
        private readonly string MEDIA = "media";
        private readonly string IMAGES = "images";
        private readonly string VIDEOS = "videos";
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

            //string newImageName = MediaHelper.MakeImageName(file.FileName.ToLower());
            //string subPath = Path.Combine(MEDIA, VIDEOS, newImageName);
            //string path = Path.Combine(ROOTPATH, subPath);

            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //    return subPath;
            //}

            string filePath = "";
            string fileName = "";


            try
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "media","videos", fileName);

               

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return fileName;
        }


        public async ValueTask<bool> DeletFileAsync(string file)
        {
            string path = Path.Combine(ROOTPATH, file);

            if (File.Exists(path))
            {
                await Task.Run(() =>
                {
                    File.Delete(path);
                });
                return true;
            }
            return false;
        }

    }
}

using Microsoft.AspNetCore.Http;

namespace Application.FileServices
{
    public interface IFileService
    {
        public ValueTask<string> UploadVideoAsync(IFormFile imagepath);
        public ValueTask<byte[]> GetVideoAsync(string path);
    }
}

using Microsoft.AspNetCore.Http;

namespace Application.FileServices
{
    public interface IFileService
    {
        ValueTask<string> UploadImageAsync(IFormFile file);
        ValueTask<byte[]> GetImageAsync(string path);
        ValueTask<bool> DeletFileAsync(string file);

    }
}

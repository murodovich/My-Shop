using Domain.Exceptions.Videos;

namespace Application.Common.Halpers
{
    public class MediaHelper
    {

        public static string MakeImageName(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);

            string[] ImageExtension = GetImageExtensions();

            if (ImageExtension.Any(x => x == fileInfo.Extension))
            {
                string extension = fileInfo.Extension;
                string name = "video_" + Guid.NewGuid() + extension;
                return name;
            }
            throw new VideoNotValid();
        }

        
        
        

        public static string[] GetImageExtensions()
        {
            return new string[]
            {
                ".mp4",
                ".mkv",
                ".mov",
                ".m4v",
                ".webm",
                ".avi",
                ".m4a",
                ".MP4",
                ".MOV",
                ".WMV",
                ".AVI",
                ".AVCHD",
                ".FLV",
                ".F4V",
                ".SWF",
                ".mkv"
            };
        }
    }
}

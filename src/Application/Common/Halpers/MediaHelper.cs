using Domain.Exceptions.Videos;

namespace Application.Common.Halpers
{
    public class MediaHelper
    {
        public static string MakeVideoImageName(string filename)
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

        public static string MakeFileName(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);

            string[] ImageExtension = GetFileExtensions();

            if (ImageExtension.Any(x => x == fileInfo.Extension))
            {
                string extension = fileInfo.Extension;
                string name = "FILE_" + Guid.NewGuid() + extension;
                return name;
            }
            throw new VideoNotValid();
        }

        public static string[] GetImageExtensions()
        {
            return new string[]
            {
            ".jpg", ".jpeg",
            ".png",
            ".bmp",
            ".svg",
            ".MP4", ".MOV",
            ".WMV",
            ".AVI",
            ".AVCHD",
            "FLV",
            "F4V",
            "SWF",
            "MKV"
            };
        }

        public static string[] GetFileExtensions()
        {
            return new string[]
            {
            ".doc",
            ".docx",
            ".xls",
            ".xlsx",
            ".pdf",
            ".txt",
            ".zip",
            ".rar",
            };
        }
    }
}

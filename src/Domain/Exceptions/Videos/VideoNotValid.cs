namespace Domain.Exceptions.Videos
{
    public class VideoNotValid : NotFoundException
    {
        public VideoNotValid()
        {
            TitleMessage = "Image not valid!";
        }
    }
}

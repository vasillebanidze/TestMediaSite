namespace WEB_API.Models
{
    public class MediaViewModel
    {
        public int MediaId { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaTitle { get; set; } = String.Empty;
        public string PictureUrl { get; set; } = string.Empty;
    }
}

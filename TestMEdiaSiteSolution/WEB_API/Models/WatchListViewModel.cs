
namespace WEB_API.Models
{
    public class WatchListViewModel
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public string MediaTitle { get; set; } = String.Empty;
        public int MediaTypeId { get; set; }
        public string MediaTypeTitle { get; set; } = null!;
        public string PictureUrl { get; set; } = String.Empty;
        public bool Watched { get; set; }
    }
}

namespace CORE.Entities
{
    public class WatchList
    {
        public int UserId { get; set; }

        public int MediaId { get; set; }
        public Media Media { get; set; } = null!;

        public bool Watched { get; set; }
    }
}

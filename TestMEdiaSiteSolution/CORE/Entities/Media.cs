namespace CORE.Entities
{
    public class Media
    {
        public int MediaId { get; set; }

        public int MediaTypeId { get; set; }
        public virtual MediaType MediaType { get; set; } = null!;

        public string MediaTitle { get; set; } = String.Empty;
        public string PictureUrl { get; set; } = String.Empty;

        public ICollection<WatchList>? WatchLists { get; set; }

    }
}

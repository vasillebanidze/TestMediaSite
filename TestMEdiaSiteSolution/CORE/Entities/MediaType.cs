namespace CORE.Entities
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string MediaTypeTitle { get; set; } = String.Empty;

        public virtual ICollection<Media>? Medias { get; set; }
    }
}

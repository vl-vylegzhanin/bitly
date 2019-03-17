namespace DataAccess.Entities
{
    public class TransformedData
    {
        public string OriginalUrl { get; set; }
        public string TrimmedUrl { get; set; }
        public int RedirectsCount { get; set; }
    }
}
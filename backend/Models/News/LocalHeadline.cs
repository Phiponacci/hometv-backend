namespace backend.Models.News
{
    public class LocalHeadline
    {
        public int Id { get; set; }
        public DateTime HeadlineDateTime { get; set; }
        public string Headline { get; set; }
        public bool IsActive { get; set; }
    }
}

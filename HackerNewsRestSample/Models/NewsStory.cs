namespace HackerNewsRestSample.Models
{
    public class NewsStory
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public string PostedBy { get; set; }
        public DateTime Time { get; set; }
        public long Score { get; set; }
        public long CommentCount { get; set; }
    }
}

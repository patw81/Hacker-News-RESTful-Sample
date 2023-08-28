namespace HackerNewsServices.Models;

public class Comment
{
    public long Id { get; set; }
    public string By { get; set; }
    public long[] Kids { get; set; }
    public long Parent { get; set; }
    public string Text { get; set; }
    public long Time { get; set; }
    public string Type { get; set; }
}
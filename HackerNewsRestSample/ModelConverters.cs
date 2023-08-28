using HackerNewsRestSample.Models;
using HackerNewsServices.Models;

namespace HackerNewsRestSample
{
    public static class ModelConverters
    {
        public static NewsStory ToNewsStory(this Story story) =>
            new()
            {
                Title = story.Title,
                Uri = story.Url,
                PostedBy = story.By,
                Time = DateTimeOffset.FromUnixTimeSeconds(story.Time).DateTime,
                Score = story.Score
            };
    }
}

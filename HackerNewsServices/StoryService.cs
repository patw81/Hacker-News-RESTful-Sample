using HackerNewsServices.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace HackerNewsServices
{
    public class StoryService : IStoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<StoryService> _logger;

        private const int MaxParallelRequests = 6;

        private static class Urls
        {
            public const string BestStories = "https://hacker-news.firebaseio.com/v0/beststories.json";
            public const string StoryItemById = "https://hacker-news.firebaseio.com/v0/item/{0}.json";
        }

        public StoryService(IHttpClientFactory httpClientFactory, ILogger<StoryService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Story>> GetBestStoriesAsync(int count) =>
            await GetBestStoriesAsync(count, default);

        public async Task<IEnumerable<Story>> GetBestStoriesAsync(int count, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();

            _logger.LogInformation("Getting best stories ids");

            var allBestStoriesResult = await client.GetFromJsonAsync<IEnumerable<long>>(Urls.BestStories, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                throw new TaskCanceledException();

            var allbestStories = allBestStoriesResult.ToList();

            _logger.LogInformation($"{allbestStories.Count} best stories ids retrieved");

            IList<Story> stories = new List<Story>();

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = MaxParallelRequests
            };

            _logger.LogInformation($"Reading stories details, max parallel requests={parallelOptions.MaxDegreeOfParallelism}");

            await Parallel.ForEachAsync(allbestStories, parallelOptions, async (storyId, token) =>
            {
                var story = await GetStoryAsync(storyId, token);

                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();

                stories.Add(story);
            });

            _logger.LogInformation($"Reading stories details completed");

            var bestStories = stories.OrderByDescending(s => s.Score).Take(count);

            return bestStories;
        }

        public async Task<Story> GetStoryAsync(long storyId) => await GetStoryAsync(storyId, default);

        public async Task<Story> GetStoryAsync(long storyId, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();

            _logger.LogInformation($"Reading story {storyId} details");

            var story = await client.GetFromJsonAsync<Story>(string.Format(Urls.StoryItemById, storyId), cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                throw new TaskCanceledException();

            _logger.LogInformation($"Reading story {storyId} details completed");

            return story;
        }

        public async Task<IEnumerable<Comment>> GetStoryCommentsAsync(long[] commentIds) => await GetStoryCommentsAsync(commentIds, default);

        public async Task<IEnumerable<Comment>> GetStoryCommentsAsync(long[] commentIds, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();
            IList<Comment> comments = new List<Comment>();

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = MaxParallelRequests
            };

            _logger.LogInformation($"Reading comments by ids: [{string.Join(", ", commentIds)}]");

            await Parallel.ForEachAsync(commentIds, parallelOptions, async (commentId, token) =>
            {
                var comment = await client.GetFromJsonAsync<Comment>(string.Format(Urls.StoryItemById, commentId), cancellationToken);

                if (token.IsCancellationRequested)
                    throw new TaskCanceledException();

                if (comment.Type == Types.Comment)
                {
                    comments.Add(comment);
                }
            });

            _logger.LogInformation($"Reading comments completed");

            return comments;
        }
    }
}
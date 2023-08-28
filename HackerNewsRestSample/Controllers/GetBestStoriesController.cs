using HackerNewsRestSample.Models;
using HackerNewsServices;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsRestSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetBestStoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;
        private readonly ILogger<GetBestStoriesController> _logger;

        public GetBestStoriesController(IStoryService storyService, ILogger<GetBestStoriesController> logger)
        {
            _storyService = storyService;
            _logger = logger;
        }

        [HttpGet("{count}")]
        public async IAsyncEnumerable<NewsStory> GetBestStories(int count)
        {
            _logger.LogInformation($"Getting best {count} stories");

            var stories = await _storyService.GetBestStoriesAsync(count);

            foreach (var story in stories)
            {
                _logger.LogInformation($"Reading comments for story {story.Id}");

                var comments = await _storyService.GetStoryCommentsAsync(story.Kids);

                var newsStory = story.ToNewsStory();

                newsStory.CommentCount = comments.Count();

                yield return newsStory;
            }
        }
    }
}
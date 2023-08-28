using HackerNewsServices.Models;

namespace HackerNewsServices;

public interface IStoryService
{
    Task<IEnumerable<Story>> GetBestStoriesAsync(int count);
    Task<IEnumerable<Story>> GetBestStoriesAsync(int count, CancellationToken cancellationToken);
    Task<Story> GetStoryAsync(long storyId);
    Task<Story> GetStoryAsync(long storyId, CancellationToken cancellationToken);
    Task<IEnumerable<Comment>> GetStoryCommentsAsync(long[] commentIds);
    Task<IEnumerable<Comment>> GetStoryCommentsAsync(long[] commentIds, CancellationToken cancellationToken);
}
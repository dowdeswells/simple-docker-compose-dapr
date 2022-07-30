using Dapr.Actors;

namespace BloggerInterface;

public interface IBlogger : IActor
{
    Task SendBlogEntry(Guid blogEntryId);
    Task<List<Guid>> GetBlogEntries();
}
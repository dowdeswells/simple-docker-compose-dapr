using BloggerInterface;
using Dapr.Actors.Runtime;

namespace Blogger;

public class Blogger : Actor, IBlogger
{
    private const string BlogEntriesStoreName = "blog_entries";
    public Blogger(ActorHost host) : base(host)
    {
    }

    public async Task SendBlogEntry(Guid blogEntryId)
    {
        await StateManager.AddOrUpdateStateAsync(
            BlogEntriesStoreName,
            new List<Guid>(),
            (key, current) =>
            {
                current.Add(blogEntryId);
                return current;
            });
    }

    public async Task<List<Guid>> GetBlogEntries()
    {
        var entries = await StateManager.TryGetStateAsync<List<Guid>>(BlogEntriesStoreName);
        if (entries.HasValue)
        {
            return entries.Value;
        }

        return new List<Guid>();
    }
}
using System.ComponentModel.DataAnnotations;
using BloggerInterface;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;

namespace SupportApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BloggerController : ControllerBase
{
    private readonly IActorProxyFactory _actorProxyFactory;

    public BloggerController(IActorProxyFactory actorProxyFactory)
    {
        _actorProxyFactory = actorProxyFactory;
    }

    [HttpGet("{bloggerId}")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetBloggerBlogEntries(
        [FromRoute][Required]string bloggerId)
    {
        var proxy = GetBloggerInstance(bloggerId);
        var blogEntries = await proxy.GetBlogEntries();
        return Ok(blogEntries);
    }

    [HttpPost("{bloggerId}")]
    public async Task<ActionResult> AddBlogEntry([FromRoute][Required]string bloggerId, [FromQuery]string text)
    {
        var proxy = GetBloggerInstance(bloggerId);
        var blogEntryId = Guid.NewGuid();
        await proxy.SendBlogEntry(blogEntryId);

        Console.WriteLine($"Blog Entry Id {blogEntryId:D}");
        return Created(String.Empty, new
        {
            BlogEntryId = blogEntryId
        });
    }

    private IBlogger GetBloggerInstance(string bloggerId)
    {
        var actorId = new ActorId(bloggerId);
        var proxy = _actorProxyFactory.CreateActorProxy<IBlogger>(actorId, "Blogger");
        return proxy;
    }
}
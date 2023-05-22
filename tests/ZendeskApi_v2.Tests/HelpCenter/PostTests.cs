using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZendeskApi_v2.Models.HelpCenter.Post;
using ZendeskApi_v2.Tests.Base;

namespace ZendeskApi_v2.Tests.HelpCenter;

[TestFixture]
[Category("HelpCenter")]
public class PostTests : TestBase
{
    private const string postTitile = "Help me!";
    private const string postDetails = "My printer is on fire!";

    [OneTimeSetUp]
    public void Setup()
    {
        var res = Api.HelpCenter.Posts.GetPostsByTopicId(Settings.Topic_ID, 100);
        foreach (var post in res.Posts?.Where(x => x.Title == "Help me!"))
        {
            Api.HelpCenter.Posts.DeletePost(post.Id.Value);
        }
    }

    [Test]
    public void CanGetPosts()
    {
        var res = Api.HelpCenter.Posts.GetPosts();
        Assert.That(res.Posts, Is.Not.Empty);
    }

    [Test]
    public void CanCreatePost()
    {
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = Api.HelpCenter.Posts.CreatePost(post);
        Assert.That(res?.Post, Is.Not.Null);
    }

    [Test]
    public void CanDeletePost()
    {
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = Api.HelpCenter.Posts.CreatePost(post);
        Assert.That(Api.HelpCenter.Posts.DeletePost(res.Post.Id.Value), Is.True);
    }

    [Test]
    public void CanGetPost()
    {
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = Api.HelpCenter.Posts.CreatePost(post);
        var get = Api.HelpCenter.Posts.GetPost(res.Post.Id.Value);
        Assert.That(get.Post.Id, Is.EqualTo(res.Post.Id));
    }

    [Test]
    public void CanGetPostForTopicId()
    {
        var res = Api.HelpCenter.Posts.GetPostsByTopicId(Settings.Topic_ID);
        Assert.That(res.Posts, Is.Not.Null);
        Assert.That(res.Posts, Is.Not.Empty);
    }

    [Test]
    public void CanUpdatePost()
    {
        var updatedPostDetails = "This has been updated";
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = Api.HelpCenter.Posts.CreatePost(post);

        res.Post.Details = updatedPostDetails;
        var updated = Api.HelpCenter.Posts.UpdatePost(res.Post);
        Assert.Multiple(() =>
        {
            Assert.That(updated?.Post, Is.Not.Null);
            Assert.That(updated.Post.Details, Is.EqualTo(updatedPostDetails));
        });
    }

    [Test]
    public async Task CanGetPostsAsync()
    {
        var res = await Api.HelpCenter.Posts.GetPostsAsync();
        Assert.That(res.Posts, Is.Not.Empty);
    }

    [Test]
    public async Task CanCreatePostAsync()
    {
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = await Api.HelpCenter.Posts.CreatePostAsync(post);
        Assert.That(res?.Post, Is.Not.Null);
    }

    [Test]
    public async Task CanDeletePostAsync()
    {
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = await Api.HelpCenter.Posts.CreatePostAsync(post);
        Assert.That(await Api.HelpCenter.Posts.DeletePostAsync(res.Post.Id.Value), Is.True);
    }

    [Test]
    public async Task CanGetPostAsync()
    {
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = await Api.HelpCenter.Posts.CreatePostAsync(post);
        var get = await Api.HelpCenter.Posts.GetPostAsync(res.Post.Id.Value);
        Assert.That(get.Post.Id, Is.EqualTo(res.Post.Id));
    }

    [Test]
    public async Task CanGetPostForTopicIdAsync()
    {
        var res = await Api.HelpCenter.Posts.GetPostsByTopicIdAsync(Settings.Topic_ID);
        Assert.That(res.Posts, Is.Not.Null);
        Assert.That(res.Posts, Is.Not.Empty);
    }

    [Test]
    public async Task CanUpdatePostAsync()
    {
        var updatedPostDetails = "This has been updated";
        var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
        var res = await Api.HelpCenter.Posts.CreatePostAsync(post);

        res.Post.Details = updatedPostDetails;
        var updated = await Api.HelpCenter.Posts.UpdatePostAsync(res.Post);
        Assert.Multiple(() =>
        {
            Assert.That(updated?.Post, Is.Not.Null);
            Assert.That(updated.Post.Details, Is.EqualTo(updatedPostDetails));
        });
    }
}

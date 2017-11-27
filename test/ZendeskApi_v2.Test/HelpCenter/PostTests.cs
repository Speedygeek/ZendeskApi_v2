using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.HelpCenter.Post;

namespace Tests.HelpCenter
{
    [TestFixture]
    [Category("HelpCenter")]
    public class PostTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.AdminEmail, Settings.AdminPassword);
        private const string postTitile = "Help me!";
        private const string postDetails = "My printer is on fire!";

        [OneTimeSetUpAttribute]
        public void setup()
        {
            var res = api.HelpCenter.Posts.GetPostsByTopicId(Settings.Topic_ID, 100);
            foreach (var post in res.Posts?.Where(x => x.Title == "Help me!"))
            {
                api.HelpCenter.Posts.DeletePost(post.Id.Value);
            }
        }

        [Test]
        public void CanGetPosts()
        {
            var res = api.HelpCenter.Posts.GetPosts();
            Assert.That(res.Posts.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanCreatePost()
        {
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = api.HelpCenter.Posts.CreatePost(post);
            Assert.That(res?.Post, Is.Not.Null);
        }

        [Test]
        public void CanDeletePost()
        {
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = api.HelpCenter.Posts.CreatePost(post);
            Assert.That(api.HelpCenter.Posts.DeletePost(res.Post.Id.Value), Is.True);
        }

        [Test]
        public void CanGetPost()
        {
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = api.HelpCenter.Posts.CreatePost(post);
            var get = api.HelpCenter.Posts.GetPost(res.Post.Id.Value);
            Assert.That(get.Post.Id, Is.EqualTo(res.Post.Id));
        }

        [Test]
        public void CanGetPostForTopicId()
        {
            var res = api.HelpCenter.Posts.GetPostsByTopicId(Settings.Topic_ID);
            Assert.That(res.Posts, Is.Not.Null);
            Assert.That(res.Posts.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanUpdatePost()
        {
            var updatedPostDetails = "This has been updated";
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = api.HelpCenter.Posts.CreatePost(post);

            res.Post.Details = updatedPostDetails;
            var updated = api.HelpCenter.Posts.UpdatePost(res.Post);

            Assert.That(updated?.Post, Is.Not.Null);
            Assert.That(updated.Post.Details, Is.EqualTo(updatedPostDetails));
        }

        [Test]
        public async Task CanGetPostsAsync()
        {
            var res = await api.HelpCenter.Posts.GetPostsAsync();
            Assert.That(res.Posts.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanCreatePostAsync()
        {
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = await api.HelpCenter.Posts.CreatePostAsync(post);
            Assert.That(res?.Post, Is.Not.Null);
        }

        [Test]
        public async Task CanDeletePostAsync()
        {
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = await api.HelpCenter.Posts.CreatePostAsync(post);
            Assert.That( await api.HelpCenter.Posts.DeletePostAsync(res.Post.Id.Value), Is.True);
        }

        [Test]
        public async Task CanGetPostAsync()
        {
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = await api.HelpCenter.Posts.CreatePostAsync(post);
            var get = await api.HelpCenter.Posts.GetPostAsync(res.Post.Id.Value);
            Assert.That(get.Post.Id, Is.EqualTo(res.Post.Id));
        }

        [Test]
        public async Task CanGetPostForTopicIdAsync()
        {
            var res = await api.HelpCenter.Posts.GetPostsByTopicIdAsync(Settings.Topic_ID);
            Assert.That(res.Posts, Is.Not.Null);
            Assert.That(res.Posts.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task CanUpdatePostAsync()
        {
            var updatedPostDetails = "This has been updated";
            var post = new Post { Title = postTitile, Details = postDetails, TopicId = Settings.Topic_ID };
            var res = await api.HelpCenter.Posts.CreatePostAsync(post);

            res.Post.Details = updatedPostDetails;
            var updated = await api.HelpCenter.Posts.UpdatePostAsync(res.Post);

            Assert.That(updated?.Post, Is.Not.Null);
            Assert.That(updated.Post.Details, Is.EqualTo(updatedPostDetails));
        }
    }
}

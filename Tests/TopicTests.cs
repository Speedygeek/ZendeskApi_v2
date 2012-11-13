using System.Linq;
using NUnit.Framework;
using ZenDeskApi_v2;
using ZenDeskApi_v2.Models.Topics;

namespace Tests
{
    [TestFixture]
    public class TopicTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);
        private long forumId;

        public TopicTests()
        {
            forumId = api.Forums.GetForums().Forums.First().Id.Value;
        }

        [Test]
        public void CanGetTopics()
        {
            var res = api.Topics.GetTopics();
            Assert.Greater(res.Count, 0);

            var byId = api.Topics.GetTopicById(res.Topics[0].Id.Value);
            Assert.AreEqual(byId.Topic.Id, res.Topics[0].Id);

            var byForum = api.Topics.GetTopicsByForum(res.Topics[0].ForumId.Value);
            Assert.AreEqual(byForum.Topics[0].ForumId, res.Topics[0].ForumId.Value);

            var byUser = api.Topics.GetTopicsByUser(Settings.UserId);
            Assert.Greater(byUser.Count, 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTopics()
        {
            var create = api.Topics.CreateTopic(new Topic()
                                                    {
                                                        ForumId = forumId,
                                                        Title = "My test topic",
                                                        Body = "testing"
                                                    });
            Assert.Greater(create.Topic.Id, 0);

            create.Topic.Title = "My New Test Topic";
            var update = api.Topics.UpdateTopic(create.Topic);
            Assert.AreEqual(update.Topic.Title, create.Topic.Title);

            Assert.True(api.Topics.DeleteTopic(create.Topic.Id.Value));
        }


        [Test]
        public void CanShowManyTopics()
        {            
            var create = api.Topics.CreateTopic(new Topic()
            {
                ForumId = forumId,
                Title = "My test topic5",
                Body = "testing5"
            });

            var get = api.Topics.GetTopics();

            var showMany = api.Topics.GetMultipleTopicsById(get.Topics.Select(x => x.Id.Value).ToList());
            Assert.AreEqual(showMany.Count, get.Count);
            Assert.Greater(showMany.Count, 0);

            Assert.True(api.Topics.DeleteTopic(create.Topic.Id.Value));
        }
    }
}
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Topics;

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
            Assert.AreEqual(showMany.Count, get.PageSize);
            Assert.Greater(showMany.Count, 0);

            Assert.True(api.Topics.DeleteTopic(create.Topic.Id.Value));
        }

        [Test]
        public void CanGetCreateUpdateAndDeleteTopicComments()
        {
            var topicId = api.Topics.GetTopics().Topics[0].Id.Value;
            var create = api.Topics.CreateTopicComment(topicId, new TopicComment()
            {
                Body = "testing topic comments!"
            });

            Assert.Greater(create.TopicComment.Id.Value, 0);

            create.TopicComment.Body = "even more testing for topic comments!";
            var update = api.Topics.UpdateTopicComment(create.TopicComment);
            Assert.AreEqual(update.TopicComment.Body, create.TopicComment.Body);

            var getComents = api.Topics.GetTopicCommentsByTopicId(topicId);
            Assert.Greater(getComents.Count, 0);

            var getByUser = api.Topics.GetTopicCommentsByUserId(Settings.UserId);
            Assert.Greater(getByUser.Count, 0);

            var getSpecific = api.Topics.GetSpecificTopicCommentByTopic(topicId, update.TopicComment.Id.Value);
            Assert.AreEqual(getSpecific.TopicComment.Id, update.TopicComment.Id);

            //This test doesn't seem to work, looks like a problem on Zendesk's side.
            //var getSpecificByUser = api.Topics.GetSpecificTopicCommentByUser(AccountsAndActivity.UserId, update.TopicComment.Id.Value);
            //Assert.AreEqual(getSpecificByUser.TopicComment.Id, update.TopicComment.Id);

            Assert.True(api.Topics.DeleteTopicComment(update.TopicComment.TopicId.Value, update.TopicComment.Id.Value));
        }

        [Test]
        public void CanGetCreateAndDeleteTopicSubscriptions()
        {
            var topicId = api.Topics.GetTopics().Topics[0].Id.Value;

            var create = api.Topics.CreateTopicSubscription(Settings.EndUserId, topicId);
            Assert.Greater(create.TopicSubscription.Id.Value, 0);

            var getAll = api.Topics.GetAllTopicSubscriptions();
            Assert.Greater(getAll.Count, 0);

            var getByTopic = api.Topics.GetTopicSubscriptionsByTopic(topicId);
            Assert.Greater(getByTopic.Count, 0);

            var getById = api.Topics.GetTopicSubscriptionById(create.TopicSubscription.Id.Value);
            Assert.AreEqual(getById.TopicSubscription.Id, create.TopicSubscription.Id);

            Assert.True(api.Topics.DeleteTopicSubscription(create.TopicSubscription.Id.Value));
        }

        [Test]
        public void CanGetCreateAndDeleteTopicVotes()
        {
            var topicId = api.Topics.GetTopics().Topics[0].Id.Value;

            var create = api.Topics.CreateVote(topicId);
            Assert.Greater(create.TopicVote.Id.Value, 0);

            var topicVotes = api.Topics.GetTopicVotes(topicId);
            Assert.Greater(topicVotes.Count, 0);

            var userVotes = api.Topics.GetTopicVotesByUser(Settings.UserId);
            Assert.Greater(userVotes.Count, 0);

            var checkVote = api.Topics.CheckForVote(topicId);
            Assert.Greater(checkVote.TopicVote.Id.Value, 0);

            Assert.True(api.Topics.DeleteVote(topicId));
        }
    }
}
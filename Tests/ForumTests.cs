using System.Linq;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Forums;

namespace Tests
{
    [TestFixture]
    public class ForumTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetForums()
        {
            var res = api.Forums.GetForums();
            Assert.Greater(res.Forums.Count, 0);

            var forum = api.Forums.GetForumById(res.Forums[0].Id.Value);
            Assert.AreEqual(forum.Forum.Id, res.Forums[0].Id.Value);
        }

        [Test]
        public void CanCreateUpdateAndDeleteForums()
        {
            var res = api.Forums.CreateForum(new Forum()
                                                 {
                                                     Name = "test forum",
                                                     ForumType = ForumTypes.Articles,
                                                     Access = ForumAccessTypes.Everybody
                                                 });
            Assert.Greater(res.Forum.Id, 0);

            res.Forum.Name = "updated test forum";
            var update = api.Forums.UpdateForum(res.Forum);
            Assert.AreEqual(update.Forum.Name, res.Forum.Name);

            Assert.True(api.Forums.DeleteForum(res.Forum.Id.Value));
        }

        [Test]
        public void CanCreateGetAndDeleteForumSubscriptions()
        {
            var subs = api.Forums.GetForumSubscriptions().ForumSubscriptions;
            foreach (var sub in subs)
            {
                api.Forums.DeleteForumSubscription(sub.Id.Value);
            }
            

            var forum = api.Forums.GetForums().Forums.First();

            var res = api.Forums.CreateForumSubscription(new ForumSubscription()
                                                             {
                                                                 ForumId = forum.Id,
                                                                 UserId = Settings.EndUserId,
                                                             });
            Assert.Greater(res.ForumSubscription.Id, 0);

            var forumSubs = api.Forums.GetForumSubscriptions();
            Assert.Greater(forumSubs.Count, 0);

            var forumSubsById = api.Forums.GetForumSubscriptionsByForumId(forumSubs.ForumSubscriptions[0].ForumId.Value);
            Assert.Greater(forumSubsById.Count, 0);

            var forumSubId = api.Forums.GetForumSubscriptionsById(forumSubs.ForumSubscriptions[0].Id.Value);
            Assert.AreEqual(forumSubId.ForumSubscription.Id, forumSubs.ForumSubscriptions[0].Id); 

            Assert.True(api.Forums.DeleteForumSubscription(res.ForumSubscription.Id.Value));

        }
    }
}
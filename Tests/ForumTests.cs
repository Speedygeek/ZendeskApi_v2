using System.Linq;
using NUnit.Framework;
using ZenDeskApi_v2;
using ZenDeskApi_v2.Models.Constants;
using ZenDeskApi_v2.Models.Forums;

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
        public void CanGetForumSubscriptions()
        {
            var res = api.Forums.GetForumSubscriptions();
            Assert.Greater(res.Count, 0);

            var res1 = api.Forums.GetForumSubscriptionsByForumId(res.ForumSubscriptions[0].ForumId.Value);
            Assert.Greater(res.Count, 0);

            var res2 = api.Forums.GetForumSubscriptionsById(res.ForumSubscriptions[0].Id.Value);
            Assert.AreEqual(res2.ForumSubscription.Id, res.ForumSubscriptions[0].Id); 
        }

        [Test]
        public void CanCreateAndDeleteForumDescription()
        {
            var forum = api.Forums.GetForums().Forums.First();

            var res = api.Forums.CreateForumSubscription(new ForumSubscription()
                                                             {
                                                                 ForumId = forum.Id,
                                                                 UserId = Settings.EndUserId,
                                                             });
            Assert.Greater(res.ForumSubscription.Id, 0);
            Assert.True(api.Forums.DeleteForumSubscription(res.ForumSubscription.Id.Value));

        }
    }
}
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
    }
}
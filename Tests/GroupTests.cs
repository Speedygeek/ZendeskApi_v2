using NUnit.Framework;
using ZenDeskApi_v2;
using ZendeskApi_v2.Models.Groups;

namespace Tests
{
    [TestFixture]
    public class GroupTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetGroups()
        {
            var res = api.Groups.GetGroups();
            Assert.Greater(res.Count, 0);
        }

        [Test]
        public void CanGetAssignableGroups()
        {
            var res = api.Groups.GetAssignableGroups();
            Assert.Greater(res.Count, 0);
        }

        [Test]
        public void CanGetGroup()
        {
            var res = api.Groups.GetGroups();
            var res1 = api.Groups.GetGroupById(res.Groups[0].Id.Value);

            Assert.AreEqual(res1.Group.Id.Value, res.Groups[0].Id.Value);
        }

        [Test]
        public void CanCreateUpdateAndDeleteGroup()
        {
            var res = api.Groups.CreateGroup("Test Group");
            Assert.True(res.Group.Id > 0);

            res.Group.Name = "Updated Test Group";
            var res1 = api.Groups.UpdateGroup(res.Group);
            Assert.AreEqual(res1.Group.Name, res.Group.Name);

            Assert.True(api.Groups.DeleteGroup(res.Group.Id.Value));
        }
    }
}
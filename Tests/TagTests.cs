using NUnit.Framework;
using ZenDeskApi_v2;

namespace Tests
{
    [TestFixture]
    public class TagTests
    {
        private ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public void CanGetCustomRoles()
        {
            var res = api.Tags.GetTags();

            Assert.Greater(res.Tags.Count, 0);
        }
    }
}